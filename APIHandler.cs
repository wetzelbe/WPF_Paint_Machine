using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

public class APIHandler
{
    private class Position
    {
        public Point LastknownPosition {get; set; }
        public bool Touching {get; set; }
    }
    private static Position _position = new Position();
    private static HttpClient client = new HttpClient();
    private static bool _clientPropertiesset = false;
    public static void UpdateData(Point NewPosition, bool touching)
    {
        _position.LastknownPosition = NewPosition;
        _position.Touching = touching;
        PushData();
    }
    public static async Task PushData()
    {
        if (!_clientPropertiesset)
        {
            client.BaseAddress = new Uri("http://localhost:4040");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _clientPropertiesset = true;
        }
        string content = JsonSerializer.Serialize<Position>(_position);
        HttpResponseMessage response = await client.PutAsync($"position", new StringContent(content, Encoding.UTF8, "application/json"));
    }


}