module Fable.Helpers.ReactNativeColorWheel

open Fable.Helpers
open Fable.Helpers.ReactNative.Props
open Fable.Import
open Fable.Import.ReactNativeColorWheel
open Fable.Core
open Fable.Core.JsInterop

type RNCW = ReactNativeColorWheel.Globals

module Props =

  type ColorWheelProps =
    | InitialColor of string
    | OnColorChange of (HSVColor -> unit)
    | OnHexColorChange of (string -> unit)
    | Style of IStyle list
    | ThumbStyle of IStyle list
    | ThumbSize of float
    | Precision of float
    | Ref of ReactNative.Ref<ReactNativeColorWheel.ColorWheel>

open Props

let private hsvToHex = import "hsvToHex" "colorsys"

type ExtColorWheelProps =
  inherit ReactNativeColorWheel.ColorWheelProps
  abstract onHexColorChange: (string -> unit) option with get, set

type private ExtColorWheel (props: ExtColorWheelProps) as this =
  inherit React.Component<ExtColorWheelProps, obj>(props)

  let mutable colorWheel: ReactNativeColorWheel.ColorWheel option = None

  let onColorChange = this.OnColorChange
  let colorWheelRef = this.ColorWheelRef

  member this.OnColorChange (color: HSVColor) =
    (defaultArg this.props.onColorChange ignore) color
    (defaultArg this.props.onHexColorChange ignore) (hsvToHex color)

  member this.ColorWheelRef (ref: ReactNativeColorWheel.ColorWheel) =
    colorWheel <- Some ref

  member this.measureOffset () =
    match colorWheel with
    | Some ref -> ref.measureOffset ()
    | None -> ()

  override this.render () =
    React.createElement(
      RNCW.ColorWheel,
      !!JS.Object.assign(
        createEmpty,
        this.props,
        createObj
          [ "onColorChange" ==> onColorChange
            "ref" ==> colorWheelRef ]),
      []
    )

let colorWheel (props: ColorWheelProps list): React.ReactElement =
  React.ofType<ExtColorWheel, _, _>
    (unbox<ExtColorWheelProps> (keyValueList CaseRules.LowerFirst props))
    []
