using System;
using System.Text;
using NotificationSamples;
using UnityEngine;
using UnityEngine.UI;


public class AppNotificationManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Reference to the notification manager.")]
	public GameNotificationsManager manager;

	[SerializeField]
	public Text notificationScheduledText;


	private const string NOTIFICATION_CHANNEL_ID = "notification_channel_id";


	private const string GAME_NOTIFICATION_CHANNEL_TITLE = "Play with the ball!";


	private const string GAME_NOTIFICATION_CHANNEL_DESCRIPTION = "Notification from GlowBall";


	private const int DISPLAY_NOTIFICATION_AFTER_DAYS = 3;


	private string smallIconName = "icon_0";


	private string largeIconName = "icon_1";

	private void Start()
	{
		this.InitializeGameChannel();
		this.ScheduleNotificationForUnactivity();
		this.DisplayPendingNotification();
	}


	private void Update()
	{
	}

	private void InitializeGameChannel()
	{
		GameNotificationChannel gameNotificationChannel = new GameNotificationChannel("notification_channel_id", "Play with the ball!", "Notification from GlowBall");
		this.manager.Initialize(new GameNotificationChannel[]
		{
			gameNotificationChannel
		});
	}

	private void ScheduleNotificationForUnactivity()
	{
		this.manager.CancelAllNotifications();
		this.ScheduleNotificationForUnactivity(3);
	}

	private void ScheduleNotificationForUnactivity(int daysIncrement)
	{
		string title = "Play with the ball!";
		string body = "Notification from GlowBall";
		DateTime deliveryTime = DateTime.UtcNow.AddDays((double)daysIncrement);
		string channelId = "notification_channel_id";
		this.SendNotification(title, body, deliveryTime, null, false, channelId, this.smallIconName, this.largeIconName);
	}

	public void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null, bool reschedule = false, string channelId = null, string smallIcon = null, string largeIcon = null)
	{
		IGameNotification gameNotification = this.manager.CreateNotification();
		if (gameNotification == null)
		{
			return;
		}
		gameNotification.Title = title;
		gameNotification.Body = body;
		gameNotification.Group = ((!string.IsNullOrEmpty(channelId)) ? channelId : "notification_channel_id");
		gameNotification.DeliveryTime = new DateTime?(deliveryTime);
		gameNotification.SmallIcon = smallIcon;
		gameNotification.LargeIcon = largeIcon;
		if (badgeNumber != null)
		{
			gameNotification.BadgeNumber = badgeNumber;
		}
		this.manager.ScheduleNotification(gameNotification).Reschedule = reschedule;
		Debug.Log(string.Format("Queued notification for unactivity with ID \"{0}\" at time {1:dd.MM.yyyy HH:mm:ss}", gameNotification.Id, deliveryTime));
	}

	private void DisplayPendingNotification()
	{
		StringBuilder stringBuilder = new StringBuilder("Pending notifications at:");
		stringBuilder.AppendLine();
		for (int i = this.manager.PendingNotifications.Count - 1; i >= 0; i--)
		{
			DateTime? deliveryTime = this.manager.PendingNotifications[i].Notification.DeliveryTime;
			if (deliveryTime != null)
			{
				stringBuilder.Append(string.Format("{0:dd.MM.yyyy HH:mm:ss}", deliveryTime));
				stringBuilder.AppendLine();
			}
		}
		this.notificationScheduledText.text = stringBuilder.ToString();
	}


}

