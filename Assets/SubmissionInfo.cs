
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-team-ok_hand";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "Team :ok_hand:";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Caitlin Grant", "caitling1@student.unimelb.edu.au"),
        new TeamMember("Elizabeth Wong", "wonges@student.unimelb.edu.au"),
        new TeamMember("Benjamin Yi", "benjaminchen@student.unimelb.edu.au"),
        new TeamMember("Sean Maher", "spmaher@student.unimelb.edu.au"), 
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "SUPERHOP";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"SUPERHOP is an exciting first-person 3D strategic running game. 
The aim of this game is to survive each timed level by making it to a goal point at the end of each map. 
Equipped with the power to fire out blocks to aid your quest, you must use your allocated amount of blocks wisely to pave a path towards the end goal, whilst collecting coins to improve your character's abilities. 
It is currently playable in two modes: levelled and endless.
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=KMayMJteF5o";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
