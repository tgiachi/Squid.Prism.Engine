---@meta

-- This file is auto-generated. Do not edit.

---@param variableName string
---@param value string
---@return nil
function ctx.add_var(variableName, value) end

---@param message string
---@return nil
function logger.info(message) end

---@param message string
---@return nil
function logger.debug(message) end

---@param message string
---@return nil
function logger.warn(message) end

---@param message string
---@return nil
function logger.error(message) end

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
function world.add_block(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid) end

---@description Set the message of the day
---@param motd string
---@return nil
function player.set_motd(motd) end

---@description Get the message of the day
---@return string[]
function player.get_motd() end

---@param text string
---@return string
function vars.r_text(text) end

