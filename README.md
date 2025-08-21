# Flappy Game

This repository contains a Unity project skeleton for a modern Flappy Bird–style game.
It demonstrates an extensible architecture using ScriptableObjects, object pooling,
and an ad mediation layer via `AdsManager` and pluggable `IAdNetworkAdapter` implementations.
Core gameplay scaffolding is provided including managers, player components and utilities.
The skeleton also includes a persistent save system and configurable ad policy for frequency-capped interstitial and rewarded ads.
Ad policy now supports consent-gated operation, ad-free purchase disabling, and run/session tracking for timestamp-based caps.

## Setup

This project stores image and audio assets using [Git LFS](https://git-lfs.com/). Install Git LFS before cloning so these files download correctly.

Project structure:

```
Assets/_Game/Scripts
├── Ads
│   ├── AdsManager.cs
│   ├── IAdNetworkAdapter.cs
│   └── Adapters
│       ├── AdmobAdapter.cs
│       ├── UnityMediationAdapter.cs
│       └── LevelPlayAdapter.cs
├── Managers
│   ├── GameManager.cs
│   ├── LevelManager.cs
│   ├── SaveSystem.cs
│   ├── DifficultyCurve.cs
│   └── ConsentManager.cs
├── Player
│   ├── PlayerController.cs
│   ├── Health.cs
│   └── BuffController.cs
├── Utilities
│   ├── RandomService.cs
│   └── ObjectPooler.cs
├── Entities
│   └── Coin.cs
└── Common
    ├── IDamageable.cs
    ├── IDamageDealer.cs
    ├── IPooled.cs
    └── IResettable.cs
```

These scripts serve as a foundation for building out gameplay, spawning systems, UI and
additional content.

## Class Responsibilities

### Game code (`Assets/_Game/Scripts`)

**Managers**

- `GameManager`: handles overall game state transitions, scoring and run lifecycle.
- `LevelManager`: tracks distance-based level progression and exposes difficulty parameters through `DifficultyCurve`.
- `SaveSystem`: serializes player data such as best score, coins, unlocks and ad timestamps.
- `AdsManager`: coordinates ad display while respecting frequency caps, consent and ad-free purchases.
- `ConsentManager`: requests and stores user consent for GDPR/ATT compliance.
- `DifficultyCurve`: scriptable object defining how scroll speed and obstacle spawns scale with level.

**Player**

- `PlayerController`: processes input and applies flap movement with vertical clamping.
- `Health`: tracks hit points, healing and death events.
- `BuffController`: manages timed player buffs and notifies listeners when they start or end.

**UI**

- `UIManager`: central toggle for menus and HUD visibility.
- `MainMenu`, `RunSummaryMenu`, `ShopMenu`, `CharacterSelectMenu`, `SettingsMenu`, `PauseMenu`: behaviours for individual menu screens.
- `HUDController`: updates health, coin, score, combo and buff timer displays.

**Utilities & Entities**

- `ObjectPooler`: reusable object pooling system for frequently spawned prefabs.
- `RandomService`: deterministic random provider with weighted selection.
- `Coin`: simple collectible that notifies the `GameManager` when picked up.
- Interfaces `IDamageable`, `IDamageDealer`, `IPooled`, `IResettable` define common behaviours for game objects.

**Ads adapters**

- `IAdNetworkAdapter`: abstraction for ad networks. `AdmobAdapter`, `UnityMediationAdapter` and `LevelPlayAdapter` provide stub implementations for testing the mediation layer.

### Sample scripts (`Assets/Scripts`)

**Managers**

- `EconomyManager`: tracks coin balance and validates purchases.
- `ScoreManager`: adds score with combo multipliers from `ComboSystem`.
- `LevelManager`: simple curve-driven speed scaling.
- `BuffController`: keeps a list of active `IBuff` instances on the player.
- `ComboSystem`: counts successive actions and resets after a delay.

**Power-ups**

- `SlowTime`: temporarily reduces global timescale.
- `DoubleCoins`: doubles coin collection for a limited time.
- `MonsterWard`: stops monster spawns while active.
- `Magnet`: attracts nearby coins.
- `ShieldBoost`: grants short invulnerability.
- `HealthPack`: restores player health on pickup.

**Obstacles**

- `Insect`: chases the player horizontally.
- `Mine`: telegraphs before exploding on contact.
- `RotatingPipe`: spins and damages on collision.
- `LaserBeam`: fires a sweeping beam after a warning period.
- `Frog`: hops periodically and harms the player on touch.

**Spawning**

- `Spawner`: instantiates random prefabs at set intervals.
- `SpawnDirector`: selects spawners based on weighted randomness and level scaling.
