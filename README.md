Gravity SDK is a tool for authorizing users who own wearables from Gravity the Studio and loading assets into a scene in Unity.

# Installation

## Install Dependency
Gravity SDK requires the [GLTFUtility](https://github.com/Siccity/GLTFUtility) package to be installed. To install it 
1. Go to “Window / Package Manager”
2. Click “+” button
3. Select “Add package from git URL”
4. Paste `https://github.com/siccity/gltfutility.git` and click “Add”
5. Click “Import”

## Download and install Gravity SDK
Download gravityts-unitysdk.unitypackage from [Releases](https://github.com/Gravity-Studio-Digital-Wear/unity-sdk/releases). To install the package
1. Go to “Assets / Import Package / Custom Package”
2. Select `gravityts-unitysdk.unitypackage`
3. Click “Import”

# Usage Example

The alpha version works only for WebGL build.

There are two scenes:
1. Plugins/Web3Unity/Scenes/WebLogin - allows users to connect a Metamask wallet
2. Plugins/GravityTS/Example/Scenes/ExampleGravitySDK - demonstrates how a user is authorized and wearables are loaded through Gravity API

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

# How it works

In order to be able to show wearables in a game (scene) you need to make the following steps.

1. Connect a user’s Metamask wallet. It gets you a user’s address and allows you to sign messages. This is done in the WebLogin scene. After successful logging in the user’s address will be available at `PlayerPrefs.GetString("Account")`.

2. Authorize a user in Gravity the Studio API. This is done in the ExampleGravitySDK scene. When a user clicks on a backpack button an inventory opens and the method `FetchGTSWearables` from `CustomizedInventory.cs` is called. For the first time it calls `_gtsManager.EstablishConnection();` which will ask a user to sign a message via Metamask and then send this message to Gravity API to verify ownership of the user’s wallet.

3. Fetch a user’s wearables from Gravity API. This is done in the same `FetchGTSWearables` method from `CustomizedInventory.cs` by calling the `_gtsManager.Wardrobe.FetchWearables();`.

4. Equip selected wearable. When a user right clicks on an item in the opened inventory and selects “Use Item” the method `HandleItemEquip()` from `AvatarManager.cs` is called. It downloads an appropriate 3D asset and loads it into the scene.
