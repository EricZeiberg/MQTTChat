using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;

namespace TestApp
{
	public partial class ChatViewController: NSViewController

	{
		MqttFactory mqttFactory;
		IMqttClient mqttClient;
		public string username { get; set; }
		string topic = "home/temperature";
		public ChatViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			mqttFactory = new MqttFactory();
			mqttClient = mqttFactory.CreateMqttClient();

			
			connect();
			// Do any additional setup after loading the view.
		}

		public override void ViewWillAppear()
        {
			View.Window.Title = "Chat (Your name is: " + username + ")";
		}

		public async Task connect()
		{
			var mqttClientOptions = new MqttClientOptionsBuilder()
					.WithTcpServer("127.0.0.1")
					.WithClientId(username)
					.Build();

			mqttClient.UseApplicationMessageReceivedHandler(e =>
			{
				try
				{
					receiveMessage(e);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message, ex);
				}
			});


			await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

			var subscribeOptions = new MQTTnet.Client.Subscribing.MqttClientSubscribeOptions();
			subscribeOptions.TopicFilters.Add(new TopicFilter { Topic = topic });

			await mqttClient.SubscribeAsync(subscribeOptions, CancellationToken.None);
			Console.WriteLine("Connected!");

		}

		public async Task sendMessage(string message)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("clientID", username);
			dict.Add("message", message);
			string JsonString = JsonConvert.SerializeObject(dict);
			var applicationMessage = new MqttApplicationMessageBuilder()
					.WithTopic(topic)
					.WithPayload(JsonString)
					.Build();

			await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);



			Console.WriteLine("MQTT application message is published.");
		}

		public async Task receiveMessage(MqttApplicationMessageReceivedEventArgs message)
		{
			string topic = message.ApplicationMessage.Topic;
			if (string.IsNullOrWhiteSpace(topic) == false)
			{
				string payload = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);


				Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
				string clientID = dict["clientID"];
				string text = dict["message"];

				Console.WriteLine($"Topic: {topic}. Message Received: {payload}");

				InvokeOnMainThread(() => {
					messageView.StringValue = messageView.StringValue + "\n" + clientID + ": " + text;
				});
			}
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		partial void buttonClicked(NSObject sender)
		{

			_ = sendMessage(textField.StringValue);
			Console.WriteLine("attempting to send message: " + textField.StringValue);
			textField.StringValue = "";

		}
	}
}