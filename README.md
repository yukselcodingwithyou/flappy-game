# Flappy Game

This repository contains a Unity project skeleton for a modern Flappy Bird–style game.
It demonstrates an extensible architecture using ScriptableObjects, object pooling,
and an ad mediation layer via `AdsManager` and pluggable `IAdNetworkAdapter` implementations.
Core gameplay scaffolding is provided including managers, player components and utilities.
The skeleton also includes a persistent save system and configurable ad policy for frequency-capped interstitial and rewarded ads.

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
