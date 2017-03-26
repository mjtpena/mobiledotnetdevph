using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Emotion;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace PDCognitive
{
	public partial class PDCognitivePage : ContentPage
	{
		string imagePath;

		public PDCognitivePage()
		{
			InitializeComponent();
		}

		async void takePhoto(object sender, System.EventArgs e)
		{
			await CrossMedia.Current.Initialize();

			var options = new StoreCameraMediaOptions()
			{
				SaveToAlbum = true,
				PhotoSize = PhotoSize.Small
			};
			var photo = await CrossMedia.Current.TakePhotoAsync(options);
			imagePath = photo?.Path;
			image.Source = photo?.Path;
		}

		async void analyzeImage(object sender, System.EventArgs e)
		{
			startActivity();
			var visionClient = new VisionServiceClient(Constants.VisionKey);
			var visionResult = await visionClient.DescribeAsync(await getPhoto());

			var confidence = visionResult.Description.Captions[0].Confidence * 100;
			var description = visionResult.Description.Captions[0].Text;

			endActivity();
			await DisplayAlert("Yehey", $"I am {confidence}% confident\n{description}", "OK");
		}

		async void analyzeFace(object sender, System.EventArgs e)
		{
			startActivity();

			var attributes = new FaceAttributeType[]
			{
				FaceAttributeType.Age,
				FaceAttributeType.Gender
			};

			var faceClient = new FaceServiceClient(Constants.FaceKey);
			var faceResult = await faceClient.DetectAsync(await getPhoto(), false, false, attributes);

			var age = faceResult[0].FaceAttributes.Age;
			var gender = faceResult[0].FaceAttributes.Gender;

			endActivity();
			await DisplayAlert("Yehey", $"Age: {age}\nGender: {gender}", "OK");
		}

		async void analyzeEmotion(object sender, System.EventArgs e)
		{
			startActivity();

			var emotionClient = new EmotionServiceClient(Constants.EmotionKey);
			var emotionResult = await emotionClient.RecognizeAsync(await getPhoto());

			var happiness = emotionResult[0].Scores.Happiness * 100;
			var sadness = emotionResult[0].Scores.Sadness * 100;

			endActivity();
			await DisplayAlert("Emotion", $"Happiness: {happiness.ToString("##.##")}\nSadness: {sadness.ToString("##.##")}", "Ok po");
		}

		async Task<Stream> getPhoto()
		{
			var request = WebRequest.Create(imagePath);
			var response = await request.GetResponseAsync();

			return response.GetResponseStream();
		}

		void startActivity()
		{
			activity.IsVisible = true;
			activity.IsRunning = true;
		}

		void endActivity()
		{
			activity.IsVisible = false;
			activity.IsRunning = false;
		}
	}
}
