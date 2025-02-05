require("metagen")
require("world")

require("assets")

users.add_user("admin", "password", true)

main.on_bootstrap(function()
    player.set_motd("Test")
    log.info("test")
end)
