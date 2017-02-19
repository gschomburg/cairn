namespace InControl
{
	// @cond nodoc
	[AutoDiscover]
	public class NexusPlayerWinProfile : UnityInputDeviceProfile
	{
		// No trigger support, sadly. They're probably out of the
		// element range Unity supports.
		//
		public NexusPlayerWinProfile()
		{
			Name = "Nexus Player Controller";
			Meta = "Nexus Player Controller on Windows";

			DeviceClass = InputDeviceClass.Controller;

			IncludePlatforms = new[] {
				"Windows",
			};

			JoystickNames = new[] {
				"GamePad",
			};

			ButtonMappings = new[] {
				new InputControlMapping {
					Handle = "A",
					Target = InputControlType.Action1,
					Source = Button10
				},
				new InputControlMapping {
					Handle = "B",
					Target = InputControlType.Action2,
					Source = Button9
				},
				new InputControlMapping {
					Handle = "X",
					Target = InputControlType.Action3,
					Source = Button8
				},
				new InputControlMapping {
					Handle = "Y",
					Target = InputControlType.Action4,
					Source = Button7
				},
				new InputControlMapping {
					Handle = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = Button6
				},
				new InputControlMapping {
					Handle = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = Button5
				},
				new InputControlMapping {
					Handle = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = Button4
				},
				new InputControlMapping {
					Handle = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = Button3
				},
				new InputControlMapping {
					Handle = "Back",
					Target = InputControlType.Select,
					Source = Button1
				},
				new InputControlMapping {
					Handle = "Start",
					Target = InputControlType.Start,
					Source = Button0
				},
				new InputControlMapping {
					Handle = "System",
					Target = InputControlType.System,
					Source = Button2
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

				DPadLeftMapping( Analog4 ),
				DPadRightMapping( Analog4 ),
				DPadUpMapping2( Analog5 ),
				DPadDownMapping2( Analog5 ),

//				new InputControlMapping {
//					Handle = "Left Trigger",
//					Target = InputControlType.LeftTrigger,
//					Source = Analog9,
//					SourceRange = InputRange.ZeroToOne,
//					TargetRange = InputRange.ZeroToOne,
//				},
//				new InputControlMapping {
//					Handle = "Right Trigger",
//					Target = InputControlType.RightTrigger,
//					Source = Analog9,
//					SourceRange = InputRange.ZeroToMinusOne,
//					TargetRange = InputRange.ZeroToOne,
//				}
			};
		}
	}
	// @endcond
}
