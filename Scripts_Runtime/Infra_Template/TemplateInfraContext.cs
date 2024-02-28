using System.Collections.Generic;

namespace Ping.Server {

    public class TemplateInfraContext {

        GameConfigTM config;

        public TemplateInfraContext() {
        }

        // Game
        public void Config_Set(GameConfigTM config) {
            this.config = config;
        }

        public GameConfigTM Config_Get() {
            return config;
        }

        // Clear
        public void Clear() {
        }

    }

}