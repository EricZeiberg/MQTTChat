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
	[Register ("ChatViewController")]
	partial class ChatViewController
	{
		[Outlet]
		AppKit.NSTextView messageView { get; set; }

		[Outlet]
		AppKit.NSTextField textField { get; set; }

		[Outlet]
		AppKit.NSTextField topicLabel { get; set; }

		[Action ("buttonClicked:")]
		partial void buttonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (messageView != null) {
				messageView.Dispose ();
				messageView = null;
			}

			if (textField != null) {
				textField.Dispose ();
				textField = null;
			}

			if (topicLabel != null) {
				topicLabel.Dispose ();
				topicLabel = null;
			}
		}
	}
}
