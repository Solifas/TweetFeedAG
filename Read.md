Instructions:

1. Ensure that you put the path to the tweets file and the path to the user file to the appsetting.json file on the following keys respectively i.e
    TweetPath
    UserFilePath
    If you want to see the unhandled exceptions you can add the path of your choice add it to:
    ErrorFile

    NB: make sure the path contains the extension of the file i.e txt


Assumptions:
1. Tweets should be atmost 140 Characters
2. Users can follow eac other
3. The names of the users are unique e.g there can only be one Alan
4. Not all users have tweets
5. User tweets are printed from the top of the file to the bottom, Assume the time of tweets is latest from top to bottom
