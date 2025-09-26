## Generate Chapters

Generate Chapters is a tool for creating a Matroska chapters file using
Significance syntax. The generated file will be saved next to the ass file.

### Configuration

There are a few things you can configure:

| Option         | Default   | Description                                                        |
| -------------- | --------- | ------------------------------------------------------------------ |
| Id Field       | `actor`   | ASS field containing the Marker                                    |
| Name Field     | `effect`  | ASS field that contains the name of the chapter                    |
| Marker         | `chapter` | Text used to identify the line as a chapter                        |
| Generate Intro | `true`    | Automatically add an "Intro" chapter if no chapter starts at `t=0` |
