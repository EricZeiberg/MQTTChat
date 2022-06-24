// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TestApp
{
	[Register ("InitialViewController")]
	partial class InitialViewController
	{
		[Outlet]
		AppKit.NSTextField topicTextField { get; set; }

		[Outlet]
		AppKit.NSTextField usernameTextField { get; set; }

		[Action ("goButtonPress:")]
		partial void goButtonPress (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (usernameTextField != null) {
				usernameTextField.Dispose ();
				usernameTextField = null;
			}

			if (topicTextField != null) {
				topicTextField.Dispose ();
				topicTextField = null;
			}
		}
	}
}
