---@meta

-- This file is auto-generated. Do not edit.

---@param variableName string
---@param value string
---@return nil
function ctx.add_var(variableName, value) end

---@param message string
---@return nil
function log.info(message) end

---@param message string
---@return nil
function log.debug(message) end

---@param message string
---@return nil
function log.warn(message) end

---@param message string
---@return nil
function log.error(message) end

---@description Called when the server is bootstrapping
---@param function string
---@return nil
function main.on_bootstrap(function) end

---@description Generate lua definitions
---@return string
function main.gen_lua_def() end

---@param eventName string
---@param function string
---@return nil
function events.on_event(eventName, function) end

---@description Load a file
---@param fileName string
---@return string
function files.load_file(fileName) end

---@description Load a file as an array
---@param fileName string
---@return string[]
function files.load_file_as_array(fileName) end

---@description Write a file
---@param fileName string
---@param content string
---@return string
function files.write_file(fileName, content) end

---@description Add a user
---@param username string
---@param password string
---@param isAdmin string
---@return boolean
function users.add_user(username, password, isAdmin) end

---@param blockId string
---@param Name string
---@param TextureId string
---@param IsSolid string
---@param IsTransparent string
---@param IsLiquid string
---@return nil
function blocks.add_block(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid) end

---@description Get 2D noise
---@param x string
---@param y string
---@param seed string
---@return number
function noise.two(x, y, seed) end

---@description Get 3D noise
---@param x string
---@param y string
---@param z string
---@param seed string
---@return number
function noise.three(x, y, z, seed) end

---@description Add a job to the scheduler, Interval in seconds
---@param name string
---@param callback string
---@param intervalInSeconds string
---@return nil
function scheduler.add_job(name, callback, intervalInSeconds) end

---@description Set the message of the day
---@param motd string
---@return nil
function player.set_motd(motd) end

---@description Get the message of the day
---@return string[]
function player.get_motd() end

---@description Linear interpolation
---@param min string
---@param max string
---@param num string
---@return number
function math_s.lerp(min, max, num) end

---@description Clamp a value between min and max
---@param value string
---@param min string
---@param max string
---@return number
function math_s.clamp(value, min, max) end

---@description Absolute value
---@param value string
---@return number
function math_s.abs(value) end

---@description Ceil value
---@param value string
---@return number
function math_s.ceil(value) end

---@description Floor value
---@param value string
---@return number
function math_s.floor(value) end

---@description Round value
---@param value string
---@return number
function math_s.round(value) end

---@description Square root
---@param value string
---@return number
function math_s.sqrt(value) end

---@description Max value
---@param a string
---@param b string
---@return number
function math_s.max(a, b) end

---@description Min value
---@param a string
---@param b string
---@return number
function math_s.min(a, b) end

---@description Get a random integer
---@param min string
---@param max string
---@return number
function random.int(min, max) end

---@description Get a random float
---@param min string
---@param max string
---@return number
function random.float(min, max) end

---@description Get a random boolean
---@return boolean
function random.bool() end

---@param callback string
---@return nil
function task_queue.add_task(callback) end

---@param callback string
---@return nil
function task_queue.add_task_main_thread(callback) end

---@param builder string
---@return nil
function world_builder.add_layer(builder) end

---@param text string
---@return string
function vars.r_text(text) end

