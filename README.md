Gravity SDK is a tool for authorizing users who own NFT wearables and loads assets into your Unity project.

# Installation

## Install Dependency
Gravity SDK WebGL example requires the [GLTFUtility](https://github.com/Siccity/GLTFUtility) package to be installed. To install it 
1. Go to “Window / Package Manager”
2. Click “+” button
3. Select “Add package from git URL”
4. Paste `https://github.com/siccity/gltfutility.git` and click “Add”
5. Click “Import”

## Download and install Gravity SDK WebGL example
Download gravitylayer-unitysdk-webgl-example.unitypackage from [Releases](https://github.com/Gravity-Studio-Digital-Wear/UnitySDK-WebGL-example/releases). To install the package
1. Go to “Assets / Import Package / Custom Package”
2. Select `gravityts-unitysdk.unitypackage`
3. Click “Import”

# Usage Example

There are two scenes:
1. Plugins/GravityLayer/Example/Scenes/WebLogin - allows users to connect a Metamask wallet
2. Plugins/GravityLayer/Example/Scenes/ExampleGravitySDK - demonstrates how a user is authorized and wearables are loaded through Gravity API

## Prepare WebGL build

To make a build
1. Go to “File / Build Settings”
2. Switch Platform to WebGL
3. Click “Player Settings”
4. Select “Player” section
5. Expand “Resolution and Presentation”
6. Select Web3GL - 2020x
7. Expand Publishing Setting
8. Switch “Compression Format” to “Disabled” and close “Player Settings”
9. Add WebLogin scene to the build (drag it from Project hierarchy window to Build Settings window)
10. Add ExampleGravitySDK scene to the build.
11. Click “Build” and type a new folder name for build.

## Run locally

In order a demo to work properly you need to run a proxy server for Gravity API. We already set it up (you will need [Node.js](https://nodejs.org/en/) and [npm](https://www.npmjs.com/) to be installed). To install the proxy server:
1. Clone it from [here](https://github.com/stfy/webgl-serve).
2. Open its folder and type:
  1. `npm i`
  2. `npm run serve`

After that, copy all content from the build folder you created above to the folder “webgl” (or you can delete “webgl” then copy the build folder and rename it to “webgl”).

That’s it. Type in your browser `http://localhost:3000/` and it should work.

For more details see [documentation](https://gravity-studio-digital-wear.github.io/gravity-docs/).