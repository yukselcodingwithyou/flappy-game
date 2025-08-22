using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FlappyGame.Tools
{
    /// <summary>
    /// Editor utility to slice sprite sheets and build prefabs, animations and controllers
    /// for a flappy-style game.
    /// </summary>
    public class FlappySpriteImporterWindow : EditorWindow
    {
        #region Configuration Fields
        // Texture fields
        public Texture2D characterSheet;
        public Texture2D obstaclesSheet;
        public Texture2D monstersSheet;
        public Texture2D powerupsSheet;

        // Grid settings
        public Vector2Int cellSize = new Vector2Int(64, 64);
        public Vector2Int padding = Vector2Int.zero;
        public Vector2Int offset = Vector2Int.zero;
        public int pixelsPerUnit = 64;

        // Animation settings
        public int characterFrameCount = 6;
        public int fps = 12;

        // Output folders
        public string spritesFolder = "Assets/Game/Sprites";
        public string animationsFolder = "Assets/Game/Animations";
        public string controllersFolder = "Assets/Game/Animators";
        public string prefabsFolder = "Assets/Game/Prefabs";

        // Sprite renderer defaults
        public string sortingLayer = "Default";
        public int orderInLayer = 0;
        #endregion

        private static readonly string[] CharacterNames = { "FlyingFox", "FlyingCat", "FlyingDog" };
        private static readonly string[] ObstacleNames = { "Mine", "LaserBeam", "Pipe" };
        private static readonly string[] MonsterNames = { "Insect", "Scorpion", "Snake" };
        private static readonly string[] PowerupNames = { "Coin_Bubble", "Magnet_Bubble", "Shield_Bubble" };

        [MenuItem("Tools/Flappy-Style Sprite Importer")]
        public static void Open()
        {
            GetWindow<FlappySpriteImporterWindow>("Flappy Sprite Importer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Sprite Sheets", EditorStyles.boldLabel);
            characterSheet = (Texture2D)EditorGUILayout.ObjectField("Character Sheet", characterSheet, typeof(Texture2D), false);
            obstaclesSheet = (Texture2D)EditorGUILayout.ObjectField("Obstacles Sheet", obstaclesSheet, typeof(Texture2D), false);
            monstersSheet = (Texture2D)EditorGUILayout.ObjectField("Monsters Sheet", monstersSheet, typeof(Texture2D), false);
            powerupsSheet = (Texture2D)EditorGUILayout.ObjectField("Powerups Sheet", powerupsSheet, typeof(Texture2D), false);

            GUILayout.Space(5);
            GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
            cellSize = EditorGUILayout.Vector2IntField("Cell Size", cellSize);
            padding = EditorGUILayout.Vector2IntField("Padding", padding);
            offset = EditorGUILayout.Vector2IntField("Offset", offset);
            pixelsPerUnit = EditorGUILayout.IntField("Pixels Per Unit", pixelsPerUnit);

            GUILayout.Space(5);
            GUILayout.Label("Animation Settings", EditorStyles.boldLabel);
            characterFrameCount = EditorGUILayout.IntField("Character Frame Count", characterFrameCount);
            fps = EditorGUILayout.IntField("FPS", fps);

            GUILayout.Space(5);
            GUILayout.Label("Output Folders", EditorStyles.boldLabel);
            spritesFolder = EditorGUILayout.TextField("Sprites", spritesFolder);
            animationsFolder = EditorGUILayout.TextField("Animations", animationsFolder);
            controllersFolder = EditorGUILayout.TextField("Animators", controllersFolder);
            prefabsFolder = EditorGUILayout.TextField("Prefabs", prefabsFolder);

        
            GUILayout.Space(10);
            if (GUILayout.Button("Import & Build Prefabs", GUILayout.Height(40)))
            {
                ImportAndBuild();
            }
        }
        private void ImportAndBuild()
        {
            try
            {
                EditorUtility.DisplayProgressBar("Flappy Importer", "Preparing folders", 0f);
                EnsureFolders();

                float step = 1f / 8f; // Rough progress estimate
                float progress = step;

                if (characterSheet != null)
                {
                    EditorUtility.DisplayProgressBar("Flappy Importer", "Slicing characters", progress);
                    SliceSheet(characterSheet, CharacterNames, (name, i) => $"{name}_{i}");
                    progress += step;

                    EditorUtility.DisplayProgressBar("Flappy Importer", "Building character assets", progress);
                    BuildCharacters();
                    progress += step;
                }
                else
                {
                    Debug.LogWarning("Character sheet not assigned; skipping characters.");
                }

                if (obstaclesSheet != null)
                {
                    EditorUtility.DisplayProgressBar("Flappy Importer", "Slicing obstacles", progress);
                    SliceSheet(obstaclesSheet, ObstacleNames, (name, i) => i == 0 ? name : $"{name}_{i}");
                    progress += step;

                    EditorUtility.DisplayProgressBar("Flappy Importer", "Building obstacle prefabs", progress);
                    BuildSpritePrefabs(obstaclesSheet, ObstacleNames);
                    progress += step;
                }
                else
                {
                    Debug.LogWarning("Obstacles sheet not assigned; skipping obstacles.");
                }

                if (monstersSheet != null)
                {
                    EditorUtility.DisplayProgressBar("Flappy Importer", "Slicing monsters", progress);
                    SliceSheet(monstersSheet, MonsterNames, (name, i) => i == 0 ? name : $"{name}_{i}");
                    progress += step;

                    EditorUtility.DisplayProgressBar("Flappy Importer", "Building monster prefabs", progress);
                    BuildSpritePrefabs(monstersSheet, MonsterNames);
                    progress += step;
                }
                else
                {
                    Debug.LogWarning("Monsters sheet not assigned; skipping monsters.");
                }

                if (powerupsSheet != null)
                {
                    EditorUtility.DisplayProgressBar("Flappy Importer", "Slicing powerups", progress);
                    SliceSheet(powerupsSheet, PowerupNames, (name, i) => i == 0 ? name : $"{name}_{i}");
                    progress += step;

                    EditorUtility.DisplayProgressBar("Flappy Importer", "Building powerup prefabs", progress);
                    BuildSpritePrefabs(powerupsSheet, PowerupNames);
                }
                else
                {
                    Debug.LogWarning("Powerups sheet not assigned; skipping powerups.");
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        #region Folder Helpers
        private void EnsureFolders()
        {
            Directory.CreateDirectory(spritesFolder);
            Directory.CreateDirectory(animationsFolder);
            Directory.CreateDirectory(controllersFolder);
            Directory.CreateDirectory(prefabsFolder);
        }
        #endregion

        #region Slicing
        private void SliceSheet(Texture2D sheet, IReadOnlyList<string> rowNames, Func<string, int, string> namingFunc)
        {
            string path = AssetDatabase.GetAssetPath(sheet);
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            if (importer == null) return;

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.spritePixelsPerUnit = pixelsPerUnit;
            importer.filterMode = FilterMode.Point;

            List<SpriteMetaData> metas = new List<SpriteMetaData>();
            int columns = (sheet.width - offset.x + padding.x) / (cellSize.x + padding.x);
            int rows = (sheet.height - offset.y + padding.y) / (cellSize.y + padding.y);
            int rowsToProcess = Mathf.Min(rows, rowNames.Count);

            for (int row = 0; row < rowsToProcess; row++)
            {
                int rowFromBottom = rows - 1 - row; // convert top-origin to bottom-origin
                for (int col = 0; col < columns; col++)
                {
                    var meta = new SpriteMetaData();
                    meta.name = namingFunc(rowNames[row], col);
                    int x = offset.x + col * (cellSize.x + padding.x);
                    int y = offset.y + rowFromBottom * (cellSize.y + padding.y);
                    meta.rect = new Rect(x, y, cellSize.x, cellSize.y);
                    meta.alignment = (int)SpriteAlignment.Center;
                    meta.pivot = new Vector2(0.5f, 0.5f);
                    metas.Add(meta);
                }
            }

            importer.spritesheet = metas.ToArray();
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
        }
        #endregion

        #region Builders
        private void BuildCharacters()
        {
            if (characterSheet == null) return;
            string sheetPath = AssetDatabase.GetAssetPath(characterSheet);
            foreach (var name in CharacterNames)
            {
                Sprite[] frames = LoadSpritesByPrefix(sheetPath, name + "_");
                if (frames.Length == 0)
                {
                    Debug.LogWarning($"No sprites found for {name} in sheet {sheetPath}");
                    continue;
                }

                if (characterFrameCount > frames.Length)
                {
                    Debug.LogWarning($"Requested {characterFrameCount} frames for {name} but only {frames.Length} available. Clamping.");
                }
                int count = Mathf.Min(characterFrameCount, frames.Length);
                frames = frames.Take(count).ToArray();

                var clip = CreateClip(name + "_Flap", frames, animationsFolder, fps);
                var controller = CreateController(name, clip, controllersFolder);
                CreateCharacterPrefab(name, frames[0], controller);
            }
        }

        private void BuildSpritePrefabs(Texture2D sheet, IReadOnlyList<string> names)
        {
            string sheetPath = AssetDatabase.GetAssetPath(sheet);
            foreach (var name in names)
            {
                Sprite sprite = LoadSprite(sheetPath, name);
                if (sprite == null)
                {
                    Debug.LogWarning($"Sprite {name} not found in sheet {sheetPath}");
                    continue;
                }
                CreateSpriteOnlyPrefab(name, sprite);
            }
        }
        #endregion

        #region Asset Creation
        private AnimationClip CreateClip(string clipName, Sprite[] frames, string folder, int frameRate)
        {
            string path = Path.Combine(folder, clipName + ".anim");
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
            if (clip == null)
            {
                clip = new AnimationClip();
                AssetDatabase.CreateAsset(clip, path);
            }
            Undo.RecordObject(clip, "Configure Clip");
            clip.frameRate = frameRate;

            EditorCurveBinding binding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = string.Empty,
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] keys = new ObjectReferenceKeyframe[frames.Length];
            for (int i = 0; i < frames.Length; i++)
            {
                keys[i] = new ObjectReferenceKeyframe
                {
                    time = i / (float)frameRate,
                    value = frames[i]
                };
            }
            AnimationUtility.SetObjectReferenceCurve(clip, binding, keys);

            var settings = AnimationUtility.GetAnimationClipSettings(clip);
            settings.loopTime = true;
            AnimationUtility.SetAnimationClipSettings(clip, settings);

            EditorUtility.SetDirty(clip);
            return clip;
        }

        private AnimatorController CreateController(string characterName, AnimationClip clip, string folder)
        {
            string path = Path.Combine(folder, characterName + ".controller");
            AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
            if (controller == null)
            {
                controller = AnimatorController.CreateAnimatorControllerAtPath(path);
            }
            Undo.RecordObject(controller, "Configure Controller");
            AnimatorStateMachine sm = controller.layers[0].stateMachine;
            AnimatorState state = sm.states.FirstOrDefault(s => s.state.name == "Flap").state;
            if (state == null)
            {
                state = sm.AddState("Flap");
            }
            state.motion = clip;
            EditorUtility.SetDirty(controller);
            return controller;
        }

        private void CreateCharacterPrefab(string name, Sprite firstFrame, RuntimeAnimatorController controller)
        {
            string path = Path.Combine(prefabsFolder, name + ".prefab");
            GameObject root;
            if (File.Exists(path))
            {
                root = PrefabUtility.LoadPrefabContents(path);
            }
            else
            {
                root = new GameObject(name);
            }

            Undo.RegisterFullObjectHierarchyUndo(root, "Configure Prefab");

            var sr = root.GetComponent<SpriteRenderer>();
            if (sr == null) sr = root.AddComponent<SpriteRenderer>();
            sr.sprite = firstFrame;
            sr.sortingLayerName = sortingLayer;
            sr.sortingOrder = orderInLayer;

            var animator = root.GetComponent<Animator>();
            if (animator == null) animator = root.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;

            if (root.GetComponent<FlappyGame.Runtime.FlapCharacter>() == null)
            {
                root.AddComponent<FlappyGame.Runtime.FlapCharacter>();
            }

            PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
        }

        private void CreateSpriteOnlyPrefab(string name, Sprite sprite)
        {
            string path = Path.Combine(prefabsFolder, name + ".prefab");
            GameObject root;
            if (File.Exists(path))
            {
                root = PrefabUtility.LoadPrefabContents(path);
            }
            else
            {
                root = new GameObject(name);
            }

            Undo.RegisterFullObjectHierarchyUndo(root, "Configure Prefab");

            var sr = root.GetComponent<SpriteRenderer>();
            if (sr == null) sr = root.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.sortingLayerName = sortingLayer;
            sr.sortingOrder = orderInLayer;

            PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
        }
        #endregion

        #region Sprite Loading Helpers
        private static Sprite LoadSprite(string sheetPath, string name)
        {
            return LoadAllSprites(sheetPath).FirstOrDefault(s => s.name == name);
        }

        private static Sprite[] LoadSpritesByPrefix(string sheetPath, string prefix)
        {
            return LoadAllSprites(sheetPath)
                .Where(s => s.name.StartsWith(prefix, StringComparison.Ordinal))
                .OrderBy(s =>
                {
                    string numPart = s.name.Substring(prefix.Length);
                    return int.TryParse(numPart, out var idx) ? idx : 0;
                })
                .ToArray();
        }

        private static Sprite[] LoadAllSprites(string sheetPath)
        {
            return AssetDatabase.LoadAllAssetRepresentationsAtPath(sheetPath)
                .OfType<Sprite>()
                .ToArray();
        }
        #endregion
    }
}
