# UniSharper.Core

[![License](https://img.shields.io/github/license/iDreamsOfGame/UniSharper.Core)](https://github.com/iDreamsOfGame/UniSharper.Core/blob/master/LICENSE)
[![OpenUPM](https://img.shields.io/npm/v/io.github.idreamsofgame.unisharper.core?label=OpenUPM&registry_uri=https://package.openupm.com)](https://openupm.com/packages/io.github.idreamsofgame.unisharper.core/)

**UniSharper.Core** is a Unity package that contains scripts, plugins and assets for general Unity application development. It provides a collection of reusable utilities — extension methods, timers, object pooling, player preferences, cross-thread invoking, audio playback, particle effect control and editor tools — so you can focus on building your game instead of rewriting common plumbing code.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
  - [Via OpenUPM (Recommended)](#via-openupm-recommended)
  - [Via Git URL](#via-git-url)
- [Usage](#usage)
- [Samples](#samples)
- [License](#license)

## Features

### Runtime (`UniSharper`)

| Module | Namespace | Description |
|--------|-----------|-------------|
| Application helpers | `UniSharper` | [`UniApplication`](UnityProject/Assets/UniSharper.Core/Runtime/UniApplication.cs) (open URL, quit, restart, loading dialog, network reachability), `DisplayScreen`, `MobileDevice`, `PlayerEnvironment`, `PlayerPath` |
| Extension methods & utilities | `UniSharper.Extensions` | Extension methods and static utilities for `GameObject`, `Component`, `Transform`, `Camera`, `Texture2D`, `Vector2/3/4`, `Quaternion`, `Rect`, `Color`, `Mathf`, `Random`, `ParticleSystem`, `RenderTexture`, `SpriteRenderer`, `UnityWebRequest`, `PlayerPrefs` and `string` |
| Timers | `UniSharper.Timers` | `Timer`, `TimerGroup` and the `TimerManager` singleton for scheduling interval-based, repeatable and time-scale-independent callbacks |
| Player preferences | `UniSharper.Preferences` | `UniPlayerPreferences<T>` — strongly-typed wrapper around `PlayerPrefs` supporting `bool`, `byte`, `char`, `decimal`, `double`, `float`, `int`, `long`, `short`, `string` and arrays, with namespace-based keys |
| Patterns | `UniSharper.Patterns` | `SingletonMonoBehaviour<T>` for lazily-initialized singleton components |
| Object pooling | `UniSharper.Pool` | `UniGameObjectPool` and `UniPrefabPool` for reusing GameObjects and prefab instances |
| Audio | `UniSharper.Audio` | `AudioPlayer` — a convenient `AudioSource` wrapper with play, pause, resume, stop and one-shot playback |
| Effects | `UniSharper.Effect` | `ParticleEffectController` and feedback components such as `CameraShake` |
| Threading | `UniSharper.Threading` | `UnityCrossThreadInvoker` for invoking methods on the main thread from worker threads, `UnityThreadSynchronizer` and a generic `ThreadEventDispatcher` |
| Android & WebGL bridges | `UniSharper`, `UniSharper.WebGL` | `AndroidWidget` and `MobileDevice` backed by an Android AAR plugin, plus `Navigator`/`Window` JavaScript bridges for WebGL |

### Editor (`UniSharper.Editor`)

- **Build & project utilities** — `BuildUtility`, `AndroidManifestUtility`, `ScriptingSymbolsUtility`, `UniPackageUtility`, `UniAssetDatabase`, `UniPrefabUtility`, `UniEditorUtility`, `UniEditorGUILayout`, `CompilationPipelineUtility` and `ScriptTemplate`.
- **Custom property attributes & drawers** — `[ReadOnlyField]`, `[FlagsEnumField]`, `[LayerField]`, `[SortingLayerField]`, `[TagField]`, `[DirectoryFilesField]` and `[ImplementedTypesField]` for a better Inspector experience.
- **Editor workflow helpers** — `InitializeOnEditorStartupAttribute`, `EditorInitializationOrderManager`, `ScriptableObjectSettingProvider` and iOS post-processing scripts (`Editor/iOS`).

### Native Plugins

- **Android** — a prebuilt `unisharper-plugin.aar` providing native widget and device integration.
- **WebGL** — `.jslib` bridge libraries (`navigator.jslib`, `window.jslib`).

## Requirements

- **Unity 2021.3** or newer
- The following companion packages (installed automatically from OpenUPM):
  - [`io.github.idreamsofgame.resharp.core`](https://openupm.com/packages/io.github.idreamsofgame.resharp.core/) — ReSharp .NET utilities
  - [`io.github.idreamsofgame.resharp.security.cryptography`](https://openupm.com/packages/io.github.idreamsofgame.resharp.security.cryptography/) — cryptography helpers
  - [`io.github.idreamsofgame.unilitjson`](https://openupm.com/packages/io.github.idreamsofgame.unilitjson/) — LitJson for Unity

## Installation

### Via OpenUPM (Recommended)

The package is published on [OpenUPM](https://openupm.com/packages/io.github.idreamsofgame.unisharper.core/).

**Option A — with [openupm-cli](https://github.com/openupm/openupm-cli):**

```sh
openupm add io.github.idreamsofgame.unisharper.core
```

The CLI automatically configures the scoped registry and adds all dependencies.

**Option B — manually in the Unity Editor:**

1. Open **Edit > Project Settings > Package Manager**.
2. Under **Scoped Registries**, add a new registry:
   - **Name:** `package.openupm.com`
   - **URL:** `https://package.openupm.com`
   - **Scope(s):** `io.github.idreamsofgame`
3. Open **Window > Package Manager**, select **My Registries** from the drop-down, find **UniSharper** and click **Install**.

Or, alternatively, edit `Packages/manifest.json` directly and add:

```json
{
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "io.github.idreamsofgame"
      ]
    }
  ],
  "dependencies": {
    "io.github.idreamsofgame.unisharper.core": "2.0.8"
  }
}
```

### Via Git URL

Install the package directly from this repository using the [UPM Git URL](https://docs.unity3d.com/Manual/upm-dependencies.html) syntax. Because the package lives in the `UnityProject/Assets/UniSharper.Core` sub-folder of this repository, you must include the `path=` query parameter.

**Option A — Package Manager UI:**

1. Open **Window > Package Manager**.
2. Click **+ > Add package from git URL...**
3. Enter:

   ```
   https://github.com/iDreamsOfGame/UniSharper.Core.git?path=UnityProject/Assets/UniSharper.Core
   ```

   To pin a specific version, append the `upm/` tag, e.g.:

   ```
   https://github.com/iDreamsOfGame/UniSharper.Core.git?path=UnityProject/Assets/UniSharper.Core#upm/2.0.8
   ```

**Option B — `Packages/manifest.json`:**

```json
{
  "dependencies": {
    "io.github.idreamsofgame.unisharper.core": "https://github.com/iDreamsOfGame/UniSharper.Core.git?path=UnityProject/Assets/UniSharper.Core#upm/2.0.8"
  }
}
```

> [!NOTE]
> Git URL installation does not resolve the companion packages listed in [Requirements](#requirements) by itself. Add the OpenUPM scoped registry described above so Unity can resolve `io.github.idreamsofgame.resharp.core`, `io.github.idreamsofgame.resharp.security.cryptography` and `io.github.idreamsofgame.unilitjson`.

## Usage

### Extension methods

```csharp
using UniSharper.Extensions;
using UnityEngine;

public class ExtensionsDemo : MonoBehaviour
{
    private void Start()
    {
        // GameObject helpers
        gameObject.TrySetActive(true);
        var rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
        var child = gameObject.FindInChildren("Target");

        // Texture helpers
        var resized = originalTexture.Resize(512, 512);

        // Formatted strings
        var vector = new Vector3(1.234567f, 2.345678f, 3.456789f);
        Debug.Log(vector.ToAccurateString()); // (1.234567, 2.345678, 3.456789)
    }
}
```

### Timers

```csharp
using UniSharper.Timers;
using UnityEngine;

public class TimerDemo : MonoBehaviour
{
    private void Start()
    {
        // Tick every 1 second, repeat 5 times, managed by TimerManager automatically.
        var timer = new Timer(1f, 5);
        timer.Started += (sender, args) => Debug.Log("Timer started!");
        timer.Ticking += (sender, args) => Debug.Log($"Tick {args.CurrentCount}.");
        timer.Start();
    }
}
```

Timers with `autoManage: true` (the default) are registered with the `TimerManager` singleton, which ticks them for you. Use `TimerGroup` to control a batch of timers together (`StartAll()`, `PauseAll()`, `StopAll()`, ...).

### Player preferences

```csharp
using UniSharper.Preferences;

public class PreferencesDemo
{
    // Preference keys are namespaced automatically by typeof(T).FullName.
    private readonly UniPlayerPreferences<int> prefs = new UniPlayerPreferences<int>();

    public void Save()
    {
        prefs.SetInt32("HighScore", 12500, saveImmediately: true); // Persist immediately
    }

    public void Load()
    {
        int highScore = prefs.GetInt32("HighScore");
        prefs.DeletePreference("HighScore"); // Or DeletePreferences() to clear all
    }
}
```

### Singleton pattern

```csharp
using UniSharper.Patterns;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public void LoadLevel(string sceneName)
    {
        // ...
    }
}

// Elsewhere
GameManager.Instance.LoadLevel("Level1");
```

### Cross-thread invoking

```csharp
using System.Threading;
using UniSharper.Threading;
using UnityEngine;

public class ThreadingDemo : MonoBehaviour
{
    [SerializeField]
    private Text label;

    private void Start()
    {
        new Thread(() =>
        {
            Thread.Sleep(1000);
            // Marshals the call onto the Unity main thread.
            UnityCrossThreadInvoker.Invoke(() => label.text = "Hello from a worker thread!");
        }).Start();
    }
}
```

### Editor attributes

```csharp
using UniSharper;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ReadOnlyField]
    [SerializeField]
    private int level; // Visible but not editable in the Inspector

    [TagField]
    [SerializeField]
    private string spawnTag; // Rendered as a tag drop-down in the Inspector

    [LayerField]
    [SerializeField]
    private int groundLayer; // Rendered as a layer drop-down in the Inspector
}
```

### Application helpers

```csharp
using UniSharper;
using UnityEngine;

public class ApplicationDemo : MonoBehaviour
{
    private void Start()
    {
        if (!UniApplication.IsInternetReachable)
        {
            UniApplication.ShowLoadingDialog("Offline", "Waiting for network...");
        }
    }

    public void OnRestartButtonClicked() => UniApplication.Restart();
}
```

## Samples

Importable examples are included in this repository, covering the custom Inspector attributes, `Texture2D` extensions, and worker-thread scenarios (`ThreadEventDispatcher`, `UnityCrossThreadInvoker`). Clone or open the `UnityProject` folder in Unity and explore the scenes under `Assets/Samples`.

## Related Packages

- [ReSharp](https://github.com/iDreamsOfGame/ReSharp) — general .NET utilities this package builds upon.
- [UniLitJson](https://github.com/iDreamsOfGame/UniLitJson) — LitJson packaged for Unity.

## Changelog

See [CHANGELOG.md](UnityProject/Assets/UniSharper.Core/CHANGELOG.md) for release notes.

## License

Released under the [MIT License](UnityProject/Assets/UniSharper.Core/LICENSE.md). Copyright © 2020–2026 Jerry Lee ([iDreamsOfGame](https://github.com/iDreamsOfGame)).
