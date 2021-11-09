# GORKO Custom Textures
Allows you to change the texture of ship sails and banners to any image accessible by URL.

## Installation
Drop this in your `bepinex/plugins` folder.

## Current Issues
- Banners have mirrored UV data at the mesh level which requires more work. Currently any image used on a banner type will slice the image in half, mirror and stretch to fit. 

## Future Features
- Custom list of short aliases for "favorite" images.
- Display custom ship names.
- More built-in banner types. 
- Optional bonus comfort for pieces with custom images. 

## Release History

### 1.1.0
- Added favorite feature. If set in your configuration file will trigger sail textures to auto-set based on who is currently driving the boat. Piloting? Sailing. Based on who is sailing the boat.
- Added configurable poll time for synchronizing textures on dedicated servers.
### 1.0.1
- Fixed the stupid readme. Its still ugly. Its still basic. 
### 1.0.0
- Initial upload. Botched the readme. Thanks, Obama.