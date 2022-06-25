## What problem does this solve?

Ever seen an error like this spammed in console?

```
UpdateNetworkGroup: Missing parent entity 7123456
```

This typically happens after a server restart if an entity was configured to save across reboots, while its parent entity was not, causing the child entity to keep looking for the parent every 2 seconds, continuously spamming the server console.

Note: Not all entities that are missing their parent will spam this error, so this plugin may reveal additional entities which can probably be killed.

## What should I do?

**Diagnose** -- Run the `debugMissingParents` command in the **server console** to see which entities are missing their parent. This information will give you a directional indicator of which circumstances are causing the issue to arise in the first place. For example, plugins might be spawning entities and parenting them to entities that are not being saved, therefore causing this issue. It could also be a vanilla Rust bug.

**Mitigate** -- Run the `debugMissingParents kill` command to kill all entities that are missing their parent.

## Commands

- `debugMissingParents` -- Prints out information about all entities that are missing their parent.
- `debugMissingParents kill` -- Kills all entities that are missing their parent.

Example output:

```
> debugMissingParents
Entities that are missing their parent:
1360496 (parent: 1360485) | BaseChair | assets/bundled/prefabs/static/chair.static.prefab @ (0.2, 0.0, -1.4)
1360518 (parent: 1360497) | BaseChair | assets/bundled/prefabs/static/chair.static.prefab @ (0.2, 0.0, -1.4)
1360678 (parent: 1360667) | BaseChair | assets/bundled/prefabs/static/chair.static.prefab @ (0.2, 0.0, -1.4)
1360769 (parent: 1360748) | BaseChair | assets/bundled/prefabs/static/chair.static.prefab @ (0.2, 0.0, -1.4)
```

```
> debugMissingParents kill
Killed 4 entities that were missing their parent.
```

```
> debugMissingParents
No entities found that are missing their parent. Hooray!
```
