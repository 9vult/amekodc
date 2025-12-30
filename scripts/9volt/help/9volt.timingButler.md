## Timing Butler

Timing Butler is based on [PhosCity's "Timing Assistant" Aegisub script](https://phoscity.github.io/Aegisub-Scripts/Timing%20Assistant/).
Timing Butler automatically snaps to keyframes, chains with the previous line, adds lead-in/out, and more - and it's fully customizable
for both you and your team.

### Usage

1. Select a line
2. Call the butler
3. Make adjustments (if needed), and move on to the next line!

*Note: Timing Butler works on one line at a time.*

### Configuration

Timing Butler can be configured at the project and personal level.

| Option                         | Default  | Description                                                                         |
| ------------------------------ | -------- | ----------------------------------------------------------------------------------- |
| Lead In                        | 120ms    | If no snapping or chaining occurs, add this amount to the event start               |
| Lead Out                       | 400ms    | If no snapping or chaining occurs, add this amount to the event end                 |
| Snap Start to Earlier Keyframe | 350ms    | How close the start time needs to be to an earlier keyframe to snap to it           |
| Snap Start to Later Keyframe   | 100ms    | How close the start time needs to be to a later keyframe to snap to it              |
| Snap End to Earlier Keyframe   | 300ms    | How close the end time needs to be to an earlier keyframe to snap to it             |
| Snap End to Later Keyframe     | 900ms    | How close the end time needs to be to a later keyframe to snap to it                |
| Chain Events                   | 620ms    | How close the start time needs to be to the previous event's end time to snap to it |
| Chain Gap                      | 0 frames | Add a gap of this many frames between events when chaining                          |

By default, project-level options are set to `-1`, so Timing Butler will fall-through to your personal settings.
Changing a project-level option will cause Timing Butler to use that instead.
