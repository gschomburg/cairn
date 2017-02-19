namespace InControl
{
	// @cond nodoc
	[AutoDiscover]
	public class XboxOneMacBTProfile : UnityInputDeviceProfile
	{
		public XboxOneMacBTProfile()
		{
			Name = "Xbox One BT Controller";
			Meta = "Xbox One BT Controller on OSX";

			DeviceClass = InputDeviceClass.Controller;
			DeviceStyle = InputDeviceStyle.XboxOne;

			IncludePlatforms = new[] {
				"OS X"
			};

			JoystickNames = new[] {
				"Unknown Xbox Wireless Controller"
			};

			ButtonMappings = new[] {
				new InputControlMapping {
					Handle = "A",
					Target = InputControlType.Action1,
					Source = Button1
				},
				new InputControlMapping {
					Handle = "B",
					Target = InputControlType.Action2,
					Source = Button2
				},
				new InputControlMapping {
					Handle = "X",
					Target = InputControlType.Action3,
					Source = Button3
				},
				new InputControlMapping {
					Handle = "Y",
					Target = InputControlType.Action4,
					Source = Button4
				},
				// new InputControlMapping {
				// 	Handle = "DPad Up",
				// 	Target = InputControlType.DPadUp,
				// 	Source = Button5
				// },
				// new InputControlMapping {
				// 	Handle = "DPad Down",
				// 	Target = InputControlType.DPadDown,
				// 	Source = Button6,
				// },
				// new InputControlMapping {
				// 	Handle = "DPad Left",
				// 	Target = InputControlType.DPadLeft,
				// 	Source = Button7
				// },
				// new InputControlMapping {
				// 	Handle = "DPad Right",
				// 	Target = InputControlType.DPadRight,
				// 	Source = Button8
				// },
				new InputControlMapping {
					Handle = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = Button5
				},
				new InputControlMapping {
					Handle = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = Button6
				},
				new InputControlMapping {
					Handle = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = Button9
				},
				new InputControlMapping {
					Handle = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = Button10
				},
				new InputControlMapping {
					Handle = "View",
					Target = InputControlType.View,
					Source = Button7
				},
				new InputControlMapping {
					Handle = "Menu",
					Target = InputControlType.Menu,
					Source = Button8
				},
				new InputControlMapping {
					Handle = "Guide",
					Target = InputControlType.System,
					Source = Button0
				}
			};

			AnalogMappings = new[] {
				LeftStickLeftMapping( Analog0 ),
				LeftStickRightMapping( Analog0 ),
				LeftStickUpMapping( Analog1 ),
				LeftStickDownMapping( Analog1 ),

				RightStickLeftMapping( Analog2 ),
				RightStickRightMapping( Analog2 ),
				RightStickUpMapping( Analog3 ),
				RightStickDownMapping( Analog3 ),

				LeftTriggerMapping( Analog4 ),
				RightTriggerMapping( Analog5 ),


			};
		}
	}
	// @endcond
}
