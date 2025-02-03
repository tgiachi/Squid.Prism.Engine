require "debug"
require "world"

users.add_user("admin", "password", true)

main.on_bootstrap(function ()
    player.set_motd("Test")
    logger.info("test")
end)


