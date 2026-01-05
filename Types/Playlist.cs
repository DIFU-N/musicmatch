using SpotifyWeb;

namespace Odyssey.MusicMatcher;

[GraphQLDescription("A curated collection of tracks designed for a specific activity or mood.")]
public class Playlist
{
    // Playlist properties go here
    [GraphQLDescription("The ID for the playlist")]
    [ID]
    public string Id { get; }

    [GraphQLDescription("The name of the playlist.")]
    public string Name { get; set; }

    [GraphQLDescription("Describes the playlist, what to expect and entices the user to listen.")]
    public string? Description { get; set; }

    [GraphQLDescription("The playlist's tracks.")]
    // public List<Track> Tracks { get; set; }
    public async Task<List<Track>> Tracks(SpotifyService spotifyService, [Parent] Playlist parent)
    {
        if (_tracks != null)
        {
            return _tracks;
        }
        else
        {
            // TODO: HTTP call
            var response = await spotifyService.GetPlaylistsTracksAsync(parent.Id);
            return response.Items.Select(item => new Track(item.Track)).ToList();
        }
    }

    private List<Track>? _tracks;

    public Playlist(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public Playlist(PlaylistSimplified obj)
    {
        Id = obj.Id;
        Name = obj.Name;
        Description = obj.Description;
    }

    public Playlist(SpotifyWeb.Playlist obj)
    {
        Id = obj.Id;
        Name = obj.Name;
        Description = obj.Description;

        // var paginatedTracks = obj.Tracks.Items;
        // var trackObjects = paginatedTracks.Select(item => new Track(item.Track));
        // Tracks = trackObjects.ToList();

        // Tracks = obj.Tracks.Items.Select(item => new Track(item.Track)).ToList();  fromm 3 lines to 1
        _tracks = obj.Tracks.Items.Select(item => new Track(item.Track)).ToList();
    }
}
