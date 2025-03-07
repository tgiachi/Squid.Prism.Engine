# Change Log

<a name="0.8.3"></a>
## [0.8.3](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.8.3) (2025-02-03)

### Features

* add ISquidPrismConfig interface to ProcessQueueConfig and NetworkServerConfig classes for better code organization and consistency ([40a6a8c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/40a6a8ca4b9f48c3a9705ebecb3f83cad0b31c94))
* add LICENSE, README.md, and Squid logo image for initial project setup and information ([2c38b7e](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2c38b7e45f3fe677debf2d3fc773ea641eafff59))
* added network protocol ([0d6d45b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/0d6d45ba1417a34f4bf434e0e06d7e935b246b5c))
* added Squid.Prism.Service.Engine library ([f25427b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/f25427b44a49b10ea3a3573bc036a694b45311d4))
* created server core library ([8889b69](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8889b696933fb989b68010c2260251444940cf2b))
* inital commit ([191b9f5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/191b9f579af9424805565086684a16d8cf4091cb))
* removed server app console for make library ([e22f2cb](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/e22f2cb60336582c31cd8a5c4c38668a02730044))
* **configs:** add process_queue_config.json with max_parallel_tasks set to 8 ([9091d0f](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/9091d0f6550bdcd32740a759827420625b2d10ee))
* **core:** add ConfigVariableAttribute, ScriptFunctionAttribute, and ScriptModuleAttribute classes to improve code organization and provide metadata for configuration variables, script functions, and script modules ([beff8be](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/beff8bec132d5879ab691e070fd3a93bc6c33d97))
* **core:** add new classes and interfaces for handling variables, metrics, events, and services ([a10191b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/a10191bf0f950ff43df74c3a60f83310ae4a6fbc))
* **core:** add ProcessQueueConfig class to handle configuration for process queue ([35f41f6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/35f41f61cfa75089ab0c9b4ec5062b6da08ac395))
* **csproj:** update project versions to 0.8.3 for consistency and to reflect latest changes and improvements ([b6a668c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/b6a668c373c4ae63ded64f2415e44d02522affd8))
* **DirectoriesConfig.cs:** add DirectoriesConfig class to manage directory paths and initialization ([91ed62c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/91ed62cbfd8c9c0e54c3394999c49dc677ea27fa))
* **http_server:** add HttpServerConfig class with default port value of 8080 ([63c6a39](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/63c6a399bedaefb6fa508113ab4f40e937fabd35))
* **IMessageTypesService.cs:** add RegisterMessage method with generic constraint ([afebead](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/afebead56e0be3620fdf223f0e71ca54fcbe523b))
* **init.lua:** add new Lua script 'bootstrap' to initialize the application ([b301c51](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/b301c516b12822bef3b9d08b6f39eb11d009164f))
* **init.lua:** add player.set_motd("Test") to set message of the day ([3fb1484](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3fb1484fcf4b7c88dfa2bf802621e0a688e33647))
* **init.lua:** add user creation for admin with username and password ([e073d3a](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/e073d3a74fc5ebd9d39f543766d279910fa8c7bb))
* **init.lua:** refactor bootstap function to use main.on_bootstrap for better ([c2b11a2](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c2b11a27fca63cad3065251750183edad545c993))
* **network:** add network_server_config.json with default port value ([f68d212](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/f68d212582cfc5e20ef046d94a7e734b226123c6))
* **network:** add NetworkMessageData struct to store message destination data ([c22cba0](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c22cba0db2fb9d61ae7cdc851d8fff93bd575443))
* **Network:** add MotdRequestMessage and MotdResponseMessage classes to handle ([821011a](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/821011aa2b05bfcb83c52b5a17c3ca846dcfa66a))
* **Network:** add RegisterNetworkMessageExtension to register network messages ([61d7b24](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/61d7b24e6717b33b45813be03b1a93abb798f2bb))
* **NetworkDispatcherService.cs:** add NetworkDispatcherService class to handle network message dispatching ([cedaa6c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/cedaa6c7ee2907f0ec647099d07737b04e337cba))
* **NetworkMessageData.cs:** rename struct from MessageDestinationData to NetworkMessageData for clarity ([57b3f3d](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57b3f3d98b596f17311f06229ae1ab18fee93445))
* **NetworkMessageMapService.cs:** add support for registering and retrieving message descriptions in NetworkMessageMapService ([3af2b79](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3af2b79349016d84ff731a14692480d829222103))
* **nuget-publish.yml:** add GitHub Actions workflow for publishing NuGet package on main branch push and manual trigger ([3efd1ed](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3efd1edb7ed98860c9e1a19eddca0ae7caaf5e48))
* **nuget-publish.yml:** add workflow steps to automate versioning and GitHub release creation ([345db71](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/345db71da0f67a8fe90649a9035763782643339a))
* **README.md:** add disclaimer section explaining the purpose of the project ([8eb3067](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8eb30675b6cca306cb2886e3facbb843686c32a5))
* **Scheduler:** add AddSchedulerJobEvent to handle adding new scheduler jobs ([95f5d84](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/95f5d84b916b33ac8a546f782b3ecf8ed7c870d7))
* **ServerServicesModule.cs:** register EventsModule to provide event handling functionality ([8868ba6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8868ba612d20ecac0ebf78f57ac523b600b06a62))
* **Squid.Prism.Engine.Core:** add ISquidPrismAutostart and ISquidPrismService interfaces ([3cae192](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3cae192a3b2a376f3affb67ee7b4ea5859876bfb))
* **Squid.Prism.Engine.sln:** add Squid.Prism.Engine.Runner project to solution ([614df2c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/614df2ca2d572842c5582c16b928d4607bb88160))
* **Squid.Prism.Engine.sln:** add Squid.Prism.Network.Server project to solution ([ea1dfa9](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ea1dfa9ecbfe9b4e578bb552cef95c04a249e48d))
* **Squid.Prism.Server:** add banner.txt file with ASCII art for server banner ([2682dd1](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2682dd125c1f84c8419fddeceb579c36916d1b01))
* **Squid.Prism.Server:** add ISquidPrismServiceProvider interface to define service provider contract ([efe41b3](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/efe41b3d46868570234e13c86c629546b97626e6))
* **Squid.Prism.Server:** add SquidPrismServerBuilder class to encapsulate server building logic for improved modularity and maintainability ([5d83c14](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/5d83c1488037db36ea4f08af7eddb1f91fa77224))
* **Squid.Prism.Server.Core:** add ContainerModuleExtension to load container modules and services dynamically ([bd30325](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/bd30325b282b58257fac0c55991bd2860426bb72))
* **tests:** add unit tests for EventBusService, ProcessQueueService, and VariablesService to ensure functionality and reliability ([ed873ba](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ed873ba2adb599351acc262e8eaa3083a6ae4785))
* **VariablesService.cs:** add default OS-related variables to improve system information collection ([db13805](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/db13805d5ebc1ebbaddeb81876522878f6b795cc))
* **workflows:** add GitHub workflow for documentation deployment on push to main branch ([24c4edc](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/24c4edca3b8df8a1b587d0d3cb4e45a2f153d368))

### Bug Fixes

* **CompressionAlgorithmType.cs:** remove duplicate 'None' enum value to prevent ambiguity and improve code clarity ([79cd1e7](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/79cd1e7cc3d353f98710c2598e56bbf255b7bbf9))
* **ConfigVariableAttribute.cs:** change Name property to be nullable to allow for null values ([9fbb1ef](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/9fbb1ef47775b08d1c88db7f21735e7a5e367458))
* **nuget-publish.yml:** add --skip-duplicate flag to 'dotnet nuget push' command to avoid pushing duplicate packages to NuGet repository ([c204701](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c204701de8b306738024b3d6b7db1cbcf0310f15))
* **nuget-publish.yml:** fix the command to generate Changelog by using 'cat' instead of 'echo' to correctly copy the content of CHANGELOG.md into a new file ([0ba5d4f](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/0ba5d4fa97415f51d49a80710e936ac5e57e8262))
* **process_queue_config.json:** change max_parallel_tasks value to use ${cpu_count} variable for flexibility ([c11351f](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c11351f46edf713f872b335f8b157c5469e0fc8d))
* **ProtobufDecoder.cs:** add logging statement to log parsed message type for debugging purposes ([57993b5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57993b5ef183a217dde9e2eaf78872ff2a5a182d))

<a name="0.8.0"></a>
## [0.8.0](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.8.0) (2025-02-03)

### Features

* added Squid.Prism.Service.Engine library ([f25427b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/f25427b44a49b10ea3a3573bc036a694b45311d4))
* removed server app console for make library ([e22f2cb](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/e22f2cb60336582c31cd8a5c4c38668a02730044))
* **configs:** add process_queue_config.json with max_parallel_tasks set to 8 ([9091d0f](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/9091d0f6550bdcd32740a759827420625b2d10ee))
* **http_server:** add HttpServerConfig class with default port value of 8080 ([63c6a39](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/63c6a399bedaefb6fa508113ab4f40e937fabd35))
* **Scheduler:** add AddSchedulerJobEvent to handle adding new scheduler jobs ([95f5d84](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/95f5d84b916b33ac8a546f782b3ecf8ed7c870d7))
* **ServerServicesModule.cs:** register EventsModule to provide event handling functionality ([8868ba6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8868ba612d20ecac0ebf78f57ac523b600b06a62))
* **Squid.Prism.Server:** add SquidPrismServerBuilder class to encapsulate server building logic for improved modularity and maintainability ([5d83c14](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/5d83c1488037db36ea4f08af7eddb1f91fa77224))
* **VariablesService.cs:** add default OS-related variables to improve system information collection ([db13805](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/db13805d5ebc1ebbaddeb81876522878f6b795cc))

### Bug Fixes

* **process_queue_config.json:** change max_parallel_tasks value to use ${cpu_count} variable for flexibility ([c11351f](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c11351f46edf713f872b335f8b157c5469e0fc8d))

<a name="0.7.0"></a>
## [0.7.0](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.7.0) (2025-02-02)

### Features

* **core:** add ConfigVariableAttribute, ScriptFunctionAttribute, and ScriptModuleAttribute classes to improve code organization and provide metadata for configuration variables, script functions, and script modules ([beff8be](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/beff8bec132d5879ab691e070fd3a93bc6c33d97))
* **init.lua:** add new Lua script 'bootstrap' to initialize the application ([b301c51](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/b301c516b12822bef3b9d08b6f39eb11d009164f))
* **init.lua:** refactor bootstap function to use main.on_bootstrap for better ([c2b11a2](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c2b11a27fca63cad3065251750183edad545c993))
* **Squid.Prism.Server:** add ISquidPrismServiceProvider interface to define service provider contract ([efe41b3](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/efe41b3d46868570234e13c86c629546b97626e6))
* **Squid.Prism.Server.Core:** add ContainerModuleExtension to load container modules and services dynamically ([bd30325](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/bd30325b282b58257fac0c55991bd2860426bb72))

### Bug Fixes

* **ConfigVariableAttribute.cs:** change Name property to be nullable to allow for null values ([9fbb1ef](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/9fbb1ef47775b08d1c88db7f21735e7a5e367458))

<a name="0.6.0"></a>
## [0.6.0](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.6.0) (2025-01-31)

### Features

* created server core library ([8889b69](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8889b696933fb989b68010c2260251444940cf2b))

<a name="0.5.1"></a>
## [0.5.1](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.5.1) (2025-01-31)

### Features

* add ISquidPrismConfig interface to ProcessQueueConfig and NetworkServerConfig classes for better code organization and consistency ([40a6a8c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/40a6a8ca4b9f48c3a9705ebecb3f83cad0b31c94))
* add LICENSE, README.md, and Squid logo image for initial project setup and information ([2c38b7e](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2c38b7e45f3fe677debf2d3fc773ea641eafff59))
* added network protocol ([0d6d45b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/0d6d45ba1417a34f4bf434e0e06d7e935b246b5c))
* inital commit ([191b9f5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/191b9f579af9424805565086684a16d8cf4091cb))
* **core:** add new classes and interfaces for handling variables, metrics, events, and services ([a10191b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/a10191bf0f950ff43df74c3a60f83310ae4a6fbc))
* **core:** add ProcessQueueConfig class to handle configuration for process queue ([35f41f6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/35f41f61cfa75089ab0c9b4ec5062b6da08ac395))
* **DirectoriesConfig.cs:** add DirectoriesConfig class to manage directory paths and initialization ([91ed62c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/91ed62cbfd8c9c0e54c3394999c49dc677ea27fa))
* **IMessageTypesService.cs:** add RegisterMessage method with generic constraint ([afebead](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/afebead56e0be3620fdf223f0e71ca54fcbe523b))
* **network:** add NetworkMessageData struct to store message destination data ([c22cba0](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c22cba0db2fb9d61ae7cdc851d8fff93bd575443))
* **NetworkDispatcherService.cs:** add NetworkDispatcherService class to handle network message dispatching ([cedaa6c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/cedaa6c7ee2907f0ec647099d07737b04e337cba))
* **NetworkMessageData.cs:** rename struct from MessageDestinationData to NetworkMessageData for clarity ([57b3f3d](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57b3f3d98b596f17311f06229ae1ab18fee93445))
* **NetworkMessageMapService.cs:** add support for registering and retrieving message descriptions in NetworkMessageMapService ([3af2b79](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3af2b79349016d84ff731a14692480d829222103))
* **nuget-publish.yml:** add GitHub Actions workflow for publishing NuGet package on main branch push and manual trigger ([3efd1ed](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3efd1edb7ed98860c9e1a19eddca0ae7caaf5e48))
* **README.md:** add disclaimer section explaining the purpose of the project ([8eb3067](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8eb30675b6cca306cb2886e3facbb843686c32a5))
* **Squid.Prism.Engine.Core:** add ISquidPrismAutostart and ISquidPrismService interfaces ([3cae192](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3cae192a3b2a376f3affb67ee7b4ea5859876bfb))
* **Squid.Prism.Engine.sln:** add Squid.Prism.Network.Server project to solution ([ea1dfa9](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ea1dfa9ecbfe9b4e578bb552cef95c04a249e48d))
* **Squid.Prism.Server:** add banner.txt file with ASCII art for server banner ([2682dd1](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2682dd125c1f84c8419fddeceb579c36916d1b01))
* **tests:** add unit tests for EventBusService, ProcessQueueService, and VariablesService to ensure functionality and reliability ([ed873ba](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ed873ba2adb599351acc262e8eaa3083a6ae4785))

### Bug Fixes

* **CompressionAlgorithmType.cs:** remove duplicate 'None' enum value to prevent ambiguity and improve code clarity ([79cd1e7](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/79cd1e7cc3d353f98710c2598e56bbf255b7bbf9))
* **nuget-publish.yml:** add --skip-duplicate flag to 'dotnet nuget push' command to avoid pushing duplicate packages to NuGet repository ([c204701](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c204701de8b306738024b3d6b7db1cbcf0310f15))
* **ProtobufDecoder.cs:** add logging statement to log parsed message type for debugging purposes ([57993b5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57993b5ef183a217dde9e2eaf78872ff2a5a182d))

<a name="0.5.1"></a>
## [0.5.1](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.5.1) (2025-01-31)

<a name="0.5.0"></a>
## [0.5.0](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.5.0) (2025-01-31)

### Features

* add ISquidPrismConfig interface to ProcessQueueConfig and NetworkServerConfig classes for better code organization and consistency ([40a6a8c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/40a6a8ca4b9f48c3a9705ebecb3f83cad0b31c94))
* add LICENSE, README.md, and Squid logo image for initial project setup and information ([2c38b7e](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2c38b7e45f3fe677debf2d3fc773ea641eafff59))
* added network protocol ([0d6d45b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/0d6d45ba1417a34f4bf434e0e06d7e935b246b5c))
* inital commit ([191b9f5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/191b9f579af9424805565086684a16d8cf4091cb))
* **core:** add new classes and interfaces for handling variables, metrics, events, and services ([a10191b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/a10191bf0f950ff43df74c3a60f83310ae4a6fbc))
* **core:** add ProcessQueueConfig class to handle configuration for process queue ([35f41f6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/35f41f61cfa75089ab0c9b4ec5062b6da08ac395))
* **DirectoriesConfig.cs:** add DirectoriesConfig class to manage directory paths and initialization ([91ed62c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/91ed62cbfd8c9c0e54c3394999c49dc677ea27fa))
* **IMessageTypesService.cs:** add RegisterMessage method with generic constraint ([afebead](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/afebead56e0be3620fdf223f0e71ca54fcbe523b))
* **network:** add NetworkMessageData struct to store message destination data ([c22cba0](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c22cba0db2fb9d61ae7cdc851d8fff93bd575443))
* **NetworkDispatcherService.cs:** add NetworkDispatcherService class to handle network message dispatching ([cedaa6c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/cedaa6c7ee2907f0ec647099d07737b04e337cba))
* **NetworkMessageData.cs:** rename struct from MessageDestinationData to NetworkMessageData for clarity ([57b3f3d](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57b3f3d98b596f17311f06229ae1ab18fee93445))
* **NetworkMessageMapService.cs:** add support for registering and retrieving message descriptions in NetworkMessageMapService ([3af2b79](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3af2b79349016d84ff731a14692480d829222103))
* **nuget-publish.yml:** add GitHub Actions workflow for publishing NuGet package on main branch push and manual trigger ([3efd1ed](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3efd1edb7ed98860c9e1a19eddca0ae7caaf5e48))
* **README.md:** add disclaimer section explaining the purpose of the project ([8eb3067](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/8eb30675b6cca306cb2886e3facbb843686c32a5))
* **Squid.Prism.Engine.Core:** add ISquidPrismAutostart and ISquidPrismService interfaces ([3cae192](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3cae192a3b2a376f3affb67ee7b4ea5859876bfb))
* **Squid.Prism.Engine.sln:** add Squid.Prism.Network.Server project to solution ([ea1dfa9](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ea1dfa9ecbfe9b4e578bb552cef95c04a249e48d))
* **Squid.Prism.Server:** add banner.txt file with ASCII art for server banner ([2682dd1](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/2682dd125c1f84c8419fddeceb579c36916d1b01))
* **tests:** add unit tests for EventBusService, ProcessQueueService, and VariablesService to ensure functionality and reliability ([ed873ba](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ed873ba2adb599351acc262e8eaa3083a6ae4785))

### Bug Fixes

* **CompressionAlgorithmType.cs:** remove duplicate 'None' enum value to prevent ambiguity and improve code clarity ([79cd1e7](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/79cd1e7cc3d353f98710c2598e56bbf255b7bbf9))
* **nuget-publish.yml:** add --skip-duplicate flag to 'dotnet nuget push' command to avoid pushing duplicate packages to NuGet repository ([c204701](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c204701de8b306738024b3d6b7db1cbcf0310f15))
* **ProtobufDecoder.cs:** add logging statement to log parsed message type for debugging purposes ([57993b5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57993b5ef183a217dde9e2eaf78872ff2a5a182d))

<a name="0.5.0"></a>
## [0.5.0](https://www.github.com/tgiachi/Squid.Prism.Engine/releases/tag/v0.5.0) (2025-01-31)

### Features

* add ISquidPrismConfig interface to ProcessQueueConfig and NetworkServerConfig classes for better code organization and consistency ([40a6a8c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/40a6a8ca4b9f48c3a9705ebecb3f83cad0b31c94))
* added network protocol ([0d6d45b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/0d6d45ba1417a34f4bf434e0e06d7e935b246b5c))
* inital commit ([191b9f5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/191b9f579af9424805565086684a16d8cf4091cb))
* **core:** add new classes and interfaces for handling variables, metrics, events, and services ([a10191b](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/a10191bf0f950ff43df74c3a60f83310ae4a6fbc))
* **core:** add ProcessQueueConfig class to handle configuration for process queue ([35f41f6](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/35f41f61cfa75089ab0c9b4ec5062b6da08ac395))
* **IMessageTypesService.cs:** add RegisterMessage method with generic constraint ([afebead](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/afebead56e0be3620fdf223f0e71ca54fcbe523b))
* **network:** add NetworkMessageData struct to store message destination data ([c22cba0](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c22cba0db2fb9d61ae7cdc851d8fff93bd575443))
* **NetworkDispatcherService.cs:** add NetworkDispatcherService class to handle network message dispatching ([cedaa6c](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/cedaa6c7ee2907f0ec647099d07737b04e337cba))
* **NetworkMessageData.cs:** rename struct from MessageDestinationData to NetworkMessageData for clarity ([57b3f3d](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57b3f3d98b596f17311f06229ae1ab18fee93445))
* **NetworkMessageMapService.cs:** add support for registering and retrieving message descriptions in NetworkMessageMapService ([3af2b79](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3af2b79349016d84ff731a14692480d829222103))
* **nuget-publish.yml:** add GitHub Actions workflow for publishing NuGet package on main branch push and manual trigger ([3efd1ed](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/3efd1edb7ed98860c9e1a19eddca0ae7caaf5e48))
* **Squid.Prism.Engine.sln:** add Squid.Prism.Network.Server project to solution ([ea1dfa9](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ea1dfa9ecbfe9b4e578bb552cef95c04a249e48d))
* **tests:** add unit tests for EventBusService, ProcessQueueService, and VariablesService to ensure functionality and reliability ([ed873ba](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/ed873ba2adb599351acc262e8eaa3083a6ae4785))

### Bug Fixes

* **CompressionAlgorithmType.cs:** remove duplicate 'None' enum value to prevent ambiguity and improve code clarity ([79cd1e7](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/79cd1e7cc3d353f98710c2598e56bbf255b7bbf9))
* **nuget-publish.yml:** add --skip-duplicate flag to 'dotnet nuget push' command to avoid pushing duplicate packages to NuGet repository ([c204701](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/c204701de8b306738024b3d6b7db1cbcf0310f15))
* **ProtobufDecoder.cs:** add logging statement to log parsed message type for debugging purposes ([57993b5](https://www.github.com/tgiachi/Squid.Prism.Engine/commit/57993b5ef183a217dde9e2eaf78872ff2a5a182d))

