namespace Fable.Import

open Fable.Core
open Fable.Import.ReactNative

[<Erase>]
module ReactNativeColorWheel =

  [<Pojo>]
  type HSVColor =
    { h: float
      s: float
      v: float }

  type ColorWheelProps =
      abstract initialColor: string option with get, set
      abstract onColorChange: (HSVColor -> unit) option with get, set
      abstract style: ViewStyle option with get, set
      abstract thumbStyle: ViewStyle option with get, set
      abstract thumbSize: float option with get, set
      abstract precision: float option with get, set
      abstract ref: (ColorWheel -> unit) option with get, set

  and ColorWheelStatic =
      inherit React.ComponentClass<ColorWheelProps>
      abstract member measureOffset: (unit -> unit)

  and ColorWheel =
      ColorWheelStatic

  
  type Globals =
      [<Import("ColorWheel", "react-native-color-wheel")>] static member ColorWheel with get(): ColorWheelStatic = jsNative and set(v: ColorWheelStatic): unit = jsNative
      