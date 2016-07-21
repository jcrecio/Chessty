namespace Graphical
{
    using System;
    using System.IO;
    using Chessty;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    public class Serializer
    {
        public void Serialize(Game game, String file)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            FileStream stream = new FileStream(file, FileMode.OpenOrCreate) { Position = 0 };

            jsonSerializer.Serialize(new BsonWriter(stream), game);
            stream.Close();
        }

        public Game Deserialize(string file)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            FileStream stream = new FileStream(file, FileMode.Open) { Position = 0 };

            JsonReader jsonReader = new BsonReader(stream);

            var game = (Game)jsonSerializer.Deserialize(jsonReader, typeof(Game));
            stream.Close();

            return game;
        }
    }
}
