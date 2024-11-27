# Differences between source code OtterUI and Z-Engine's OtterUI

## new Data Types

- new struct GSPP : ControlData + 44 bytes
  - uint TextureID
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint Footer (always 12345678)
- new struct SPPL : ControlLayout + 28 bytes
  - uint mColor
  - uint ???
  - uint ???
  - uint mColor2
  - uint ???
  - uint ???
  - uint Footer (always 12345678)
- new struct ARCC : ControlData + 28 bytes
  - uint ???
  - uint ???
  - uint ???
  - uint ???
  - uint ??? (are these degrees?)
  - uint ??? (are these degrees?)
  - uint mFlag (either 1 or 0)
- new struct ARLT : ControlLayout + 20 bytes (always in KeyFrameData?)
  - uint ???
  - uint ???
  - uint ???
  - uint ??? (are these degrees?)
  - uint ??? (are these degrees?)

## Notes

- ARLT and ARCC might be related?
- is SPPL some kind of gradient?

## Changes

- KeyFrameData has a ControlLayout
- FontData works completly differently
- GroupLayout may have no color
