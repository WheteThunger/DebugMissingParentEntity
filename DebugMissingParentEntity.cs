using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Plugins
{
    [Info("Debug Missing Parent Entity", "WhiteThunder", "1.0.0")]
    [Description("Identifies and optionally kills entities that are missing their parent.")]
    public class DebugMissingParentEntity : CovalencePlugin
    {
        [Command("debugMissingParents")]
        private void Command(IPlayer player, string cmd, string[] args)
        {
            if (!player.IsServer)
                return;

            var foundObjects = new List<BaseNetworkable>();
            var reportLines = new List<string>();

            foreach (var networkable in BaseNetworkable.serverEntities)
            {
                if (!networkable.parentEntity.IsSet())
                    continue;

                var parentEntityId = networkable.parentEntity.uid;
                if (parentEntityId == 0)
                    continue;

                //var parentEntityId = networkable.parentEntity.uid;
                var parent = networkable.GetParentEntity();
                if (parent == null && networkable != null && !networkable.IsDestroyed)
                {
                    foundObjects.Add(networkable);
                    reportLines.Add($"{networkable.net.ID} (parent: {parentEntityId}) | {networkable.GetType()} | {networkable.PrefabName} @ {networkable.transform.position}");
                }
            }

            if (reportLines.Count == 0)
            {
                player.Reply($"No entities found that are missing their parent. Hooray!");
                return;
            }

            var reportString = string.Join("\n", reportLines);
            player.Reply($"Entities that are missing their parent:\n{reportString}");

            if (args.FirstOrDefault()?.ToLower() == "kill")
            {
                foreach (var networkable in foundObjects)
                {
                    networkable.Kill();
                }
                player.Reply($"Killed {foundObjects.Count} entities that were missing their parent.");
            }
        }
    }
}