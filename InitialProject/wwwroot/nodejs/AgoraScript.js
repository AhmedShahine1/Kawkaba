import AgoraRTC from "agora-rtc-sdk-ng";

const AgoraRTC = require("agora-rtc-sdk-ng");

async function joinChannel(appId, channel, token, uid) {
    let rtc = {
        localAudioTrack: null,
        client: null
    };

    // Initialize the client
    rtc.client = AgoraRTC.createClient({ mode: "rtc", codec: "vp8" });

    try {
        // Join the channel
        await rtc.client.join(appId, channel, token, uid);

        // Log successful join
        console.log("Joined channel:", channel);

        // Create a local audio track
        rtc.localAudioTrack = await AgoraRTC.createMicrophoneAudioTrack();
        await rtc.client.publish([rtc.localAudioTrack]);

        console.log("Audio track published!");
        return { success: true, channelId: channel };
    } catch (error) {
        console.error("Error joining channel:", error);
        return { success: false, error: error.message };
    }
}

// Get parameters from command-line arguments
const appId = process.argv[2];
const channel = process.argv[3];
const token = process.argv[4];
const uid = process.argv[5];

// Call the joinChannel function and output the result
joinChannel(appId, channel, token, uid).then(result => {
    console.log(JSON.stringify(result));
});
