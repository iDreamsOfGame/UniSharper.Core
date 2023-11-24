# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



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
 - Initial unity project and adnroid plugin project for package distribution.