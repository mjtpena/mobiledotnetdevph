using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace XamarinFiestaCognitive
{
    public partial class MainPage : ContentPage
    {
        MediaFile imageFile;
        public MainPage()
        {
            InitializeComponent();
        }
        
        private async void btnCapture_Clicked(object sender, EventArgs e)
        {
            imageFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Name = "photoFace.jpg"
            });

            imgCaptured.Source = imageFile.Path;
        }

        private async void btnAnalyzeFace_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<PersonFace> detectedFaces = new ObservableCollection<PersonFace>();
            
            var faceService = new FaceServiceClient("65beb1d0c8844590bd52bc23c0ceac25");

            Face[] faces = await faceService.DetectAsync(imageFile.GetStream(), false, false, new FaceAttributeType[] { FaceAttributeType.Age, FaceAttributeType.Gender });

            foreach (var face in faces)
            {
                detectedFaces.Add(new PersonFace
                {
                    Age = face.FaceAttributes.Age.ToString(),
                    Gender = face.FaceAttributes.Gender.ToString()
                });
            }

            PersonFace mainFace = detectedFaces.FirstOrDefault();

            await DisplayAlert("Yeheyy", "Age: " + mainFace.Age + " Gender: " + mainFace.Gender, "Okayyy");
        }

        private async void btnCheckEmotion_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<PersonEmotion> DetectedEmotions = new ObservableCollection<PersonEmotion>();

            var emotionClient = new EmotionServiceClient("f6b14f92fda541d68e759c37abaa7f32");

            Emotion[] emotionResults = await emotionClient.RecognizeAsync(imageFile.GetStream());

            foreach (var emotion in emotionResults)
            {
                DetectedEmotions.Add(new PersonEmotion()
                {
                    Happiness = emotion.Scores.Happiness,
                    Fear = emotion.Scores.Fear,
                    Disgust = emotion.Scores.Disgust,
                    Sadness = emotion.Scores.Sadness
                });
            }

            PersonEmotion mainEmotion = DetectedEmotions.FirstOrDefault();

            if (mainEmotion.Happiness > 0.5f)
            {
                await DisplayAlert("Emotion", "You are happy", "OK");
            }
            else
            {
                await DisplayAlert("Emotion", "You are not happy", "OK");
            }
        }
    }
}
