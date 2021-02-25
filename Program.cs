using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace face_verify_test
{
    class Program
    {
        const string SUBSCRIPTION_KEY = "KEY";
        const string ENDPOINT = "https://faceverificaiton.cognitiveservices.azure.com/";

        private static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        public static void VerifyFace(IFaceClient client, string face1URL, string face2URL)
        {
            var fId1 = client.Face.DetectWithUrlAsync(face1URL, recognitionModel: "recognition_03", detectionModel: DetectionModel.Detection02).Result[0].FaceId;
            var fId2 = client.Face.DetectWithUrlAsync(face2URL, recognitionModel: "recognition_03", detectionModel: DetectionModel.Detection02).Result[0].FaceId;

            VerifyResult verifyResult = verifyResult = client.Face.VerifyFaceToFaceAsync(fId1.Value, fId2.Value).Result;

            Console.WriteLine($"Face confidence: {verifyResult.Confidence}.");
        }

        static void Main(string[] args)
        {
            IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);

            Console.Write("Face 1 URL: ");
            string face1URL = Console.ReadLine();

            Console.Write("Face 2 URL: ");
            string face2URL = Console.ReadLine();

            VerifyFace(client, face1URL, face2URL);

        }

    }
}
