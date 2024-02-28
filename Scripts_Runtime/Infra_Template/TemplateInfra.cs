using System.Threading.Tasks;

namespace Ping.Server {

    public static class TemplateInfra {

        public static async Task LoadAssets(TemplateInfraContext ctx) {

            {
                if (!FileHelper.Exists("GameConfig.bytes")) {
                    PLog.LogError("GameConfig.bytes not found");
                    return;
                }

                byte[] buffer = await FileHelper.LoadBytesAsync("GameConfig.bytes");

                GameConfigTM configTM = new GameConfigTM();
                int offset = 0;
                configTM.FromBytes(buffer, ref offset);

                ctx.Config_Set(configTM);
            }

        }

        public static void Release(TemplateInfraContext ctx) {
        }

    }

}