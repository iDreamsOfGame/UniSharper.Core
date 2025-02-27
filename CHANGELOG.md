# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [1.25.0] - 2025-02-27

### Changed

- Rename class **PlayerPreferences** to **UniPlayerPreferences**.



### Added

- Adds abstract class **PlayerPreferences**.



## [1.24.9] - 2025-01-22

### Changed

- Removed property **PlayerEnvironment.IsEditorPlatform**.
- Rename property **PlayerEnvironment.IsLinuxEditorPlatform** to **PlayerEnvironment.IsLinuxEditor**.
- Rename property **PlayerEnvironment.IsMacOSXEditorPlatform** to **PlayerEnvironment.IsMacOSXEditor**.
- Rename property **PlayerEnvironment.IsWindowsEditorPlatform** to **PlayerEnvironment.IsWindowsEditor**.



## [1.24.7] - 2024-10-31

### Added

- Implements extension method **TrySetActive** for **GameObject**.



## [1.24.5] - 2024-10-16

### Changed

- Added static properties **Is64BitProcess** to class **PlayerEnvironment**.



## [1.24.3] - 2024-09-12

### Added

- Added properties **Width** and **Height** to class **DisplayScreen**.



## [1.24.0] - 2024-09-02

### Added

- Added class **Window** including properties and methods to access WebGL window object.



## [1.23.0] - 2024-08-15

### Added

- Added class **Navigator** including properties and methods to access WebGL navigator object.



## [1.22.6] - 2024-07-02

### Fixed

- Fixed compilation errors in Unity 2019.



## [1.22.0] - 2024-05-30

### Added

- Added class **ParticleSystemExtensions** including extension methods collection of **ParticleSystem**.



### Fixed

- Fixed the bug of **ParticleEffectController**.



## [1.21.7] - 2024-04-28

### Changed

- Improved the performance of **ParticleEffectController**.



## [1.21.6] - 2024-04-25

### Fixed

- Fixed bug of **float.TryParse** in different culture return different result.



## [1.21.0] - 2024-02-20

### Removed

- Removes class **NetworkUtility**.

### Added

- Adds class **UniApplication**.
- Adds method **UniApplication.Quit**.

### Changed

- Moves method **NetworkUtility.OpenURL** to **UniApplication.OpenURL**.



## [1.20.0] - 2023-12-28

### Changed

- Changes package dependency from **jillejr.newtonsoft.json-for-unity** to **com.unity.nuget.newtonsoft-json**.

### Added

- Adds package dependency **jillejr.newtonsoft.json-for-unity.converters**.



## [1.18.0] - 2023-12-21

### Added

- Adds **Sprite-Flash.shader** and **Sprite-Flash.mat**.



## [1.17.0] - 2023-12-20

### Added

- Adds class **FileExtensions** that include common file extension constants.



## [1.16.0] - 2023-11-24

### Added

- Adds class **SearchPatterns** that include common file search patterns.



## [1.15.0] - 2023-11-24

### Added

- Adds utility class **PlayerPath** to performs operations on string instances that contain file or directory path information in Unity player.



## [1.14.0] - 2023-07-12

### Added

- Adds utility class **ScriptingSymbolsUtility** to handle **scripting symbols**.



## [1.13.0] - 2022-12-15

### Added

- Adds **DisplayScreen** to access display screen information.
- Adds **MobileDevice** to implement mobile specific function.
- Adds some extension functions and utility classes.
- Adds **MissingScriptsCleaner** to clear missing scripts on **GameObject**.
- Adds editor feature **Copy Path in Hierarchy**.



## [1.12.0] - 2022-12-07

### Added

- Adds so many extension methods and utility methods.

### Fixed

- Fixed some bugs.



## [1.11.0] - 2021-07-15

### Added

- Adds attributes **TagFieldAttribute**, **SortingLayerFieldAttribute**, **LayerFieldAttribute**.
- Adds utility method **Vector2Utility.Rotate**.



## [1.10.0] - 2021-06-10

### Added

- Adds utility class **PlayerPrefsUtility**.
- Adds class **PlayerPreferences**.



## [1.9.0] - 2021-03-07

### Added

- Adds class **NetworkUtility**.

### Changed

- Use Java source file to replace aar file.
- Change asmdef file naming as [Unity User Manual](https://docs.unity3d.com/2019.4/Documentation/Manual/cus-layout.html).

### Fixed

- Fixed some bugs.




## [1.8.0] - 2020-09-18

### Changed

- Refactor plugin folder structures follow the [Unity Manual](https://docs.unity3d.com/Manual/cus-layout.html).



### Added

- Adds class **CameraShake** to implement camera shake post processing effect.



## [1.7.0] - 2020-08-08

### Added

- Adds **Vector2Utility.GenerateAchimedeanSpiralPoints** to generate points of Achimedean Spiral.
- Adds **Vector2Utility.GenerateLogarithmicSpiralPoints** to generate points of Logarithmic Spiral.

- Adds **Vector2Utility.GenerateQuadraticBezierPoints** to generate points of quadratic bezier.
- Adds **Vector2Utility.GenerateCubicBezierPoints** to generate points of cubic bezier.



## [1.6.0] - 2020-05-21

### Added

- Adds **Vector3Utility.GenerateAchimedeanSpiralPoints** to generate points of Achimedean Spiral.
- Adds **Vector3Utility.GenerateLogarithmicSpiralPoints** to generate points of Logarithmic Spiral.

- Adds **Vector3Utility.GenerateQuadraticBezierPoints** to generate points of quadratic bezier.
- Adds **Vector3Utility.GenerateCubicBezierPoints** to generate points of cubic bezier.



## [1.4.1] - 2020-05-11

### Added

- Adds **CameraExtensions.CaptureScreenshotTexture** to capture screenshot of the camera view.
- Adds **RenderTextureExtensions.ToTexture** to convert **RenderTexture** to **Texture2D**.
- Adds **Texture2DExtensions.BlendTexture** to composite background texture and foreground texture.

### Changed

- Change dependency of **Json.NET for Unity** to **jillejr.newtonsoft.json-for-unity**.



## [1.3.0] - 2020-05-06

### Added

- Adds **ParticleEffectController** to control visual effect of particle system.



## [1.2.0] - 2020-04-26

### Added

- Adds **UnityCrossThreadInvoker** to invoke methods in other threads.



## [1.1.1] - 2020-04-24

### Changed

- Adds **ScriptTemplate.LoadScriptTemplateFile** method to load script template file.



## [1.1.0] - 2020-04-23

### Added
- Adds class **AudioPlayer** to play audio source.

  

## [1.0.0] - 2020-04-21
### Added

 - Initial unity project and adnroid plugin project for package distribution.