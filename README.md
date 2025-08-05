[![openupm](https://img.shields.io/npm/v/com.sxm.ui-factory?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.sxm.ui-factory/)
[![Unity Version](https://img.shields.io/badge/unity-2020.3%2B-coral)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT-green)](https://github.com/sxm-sxpxxl/ui-factory/blob/master/LICENSE.md)

<p align="left">
  <img alt="line-chart-preview" src="https://github.com/sxm-sxpxxl/ui-factory/assets/37039414/3459c8f9-7d07-4830-b0df-2a4a48c9e01a" />
</p>

## About

Factory of various UI components, from primitives (line, point) to complex objects (graph). UI meshes support conversion to UIElements format.

## Supported UI components

- [x] Line
  - Solid
  - Dash
- [x] Point
  - by filling out:
    - Filled
    - Outlined
  - by shape:
    - Circle
    - Square
    - Triangle
- [x] Series
  - Line
  - Point
- [x] Graph

## How To Install
### Install via OpenUPM
The package is available on the [openupm registry](https://openupm.com/). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.sxm.ui-factory
```

### Install via Git URL
Please add the following line to the manifest file (`Packages/manifest.json`) to the `dependencies` section:

```
"com.sxm.ui-factory": "https://github.com/sxm-sxpxxl/ui-factory.git"
```

or just download and unzip the repository into the `Packages` folder.
