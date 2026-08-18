using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#if DEBUG
namespace DescentIntoUnderworld.Content.Dev
{
    // Comando de desarrollo para reaplicar SetDefaults a las instancias existentes
    // Uso en el chat del juego: /reapplydefaults
    public class DevCommands : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "reapplydefaults";
        public override string Usage => "/reapplydefaults";
        public override string Description => "Reaplica SetDefaults a los items del mod en mundo e inventarios (útil tras Hot Reload).";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int count = 0;
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item it = Main.item[i];
                if (it is null) continue;
                if (!it.active) continue;

                // Si el item pertenece a este mod, reaplicar defaults
                // Se usa comprobación por namespace de la clase ModItem para evitar problemas
                // cuando Hot Reload cambia instancias de Mod (comparar Mod puede fallar).
                bool isOurModItem = false;
                if (it.ModItem != null)
                {
                    var miType = it.ModItem.GetType();
                    if (miType != null && miType.Namespace != null && miType.Namespace.StartsWith("DescentIntoUnderworld.Content.Items"))
                        isOurModItem = true;
                }
                if (isOurModItem)
                {
                    int type = it.type;
                    it.SetDefaults(type);
                    count++;

                    // En multijugador hay que sincronizar el item concreto
                    if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i);
                    }
                }
            }

            caller.Reply($"{count} items reinitialized.");
        }
    }
}
#endif
