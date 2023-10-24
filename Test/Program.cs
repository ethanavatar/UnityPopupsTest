using gdio.common.objects;
using gdio.unity_api.v2;

var autoplay = false;
var api = new ApiClient();

// Setup connection
api.Connect("localhost", autoplay: autoplay);

api.Wait(1000);
api.EnableHooks(HookingObject.ALL);

// Test
api.Wait(1000);
var fps = (int) api.GetLastFPS();

Console.WriteLine("FPS: " + fps);

// Popup listener
var popupListener = api.ScheduleScript("""
local obj = ResolveObject("//*[@name='Popup(Clone)']")
if obj != nil then
    Notify(true)
end
""", ScriptExecutionMode.EveryNthFrames, fps * 5);

// Flag to indicate popup exists
var isPopup = false;
int popupCount = 0;

// Trigger callback when a popup is detected
api.ScriptSignal += (sender, args) => {
    Console.WriteLine("Found a popup!");
    isPopup = true;
};

void HandlePopup() {
    api.Wait(300);
    // Click close button
    api.ClickObject(MouseButtons.LEFT, "//*[@name='Close Button']", 10);

    // Reset popup flag
    isPopup = false;
    popupCount++;
}

// Main loop
while (popupCount < 10) {
    Console.WriteLine("Doing other stuff...");
    Thread.Sleep(1000);

    if (isPopup) {
        HandlePopup();
    }
}

api.UnscheduleScript(popupListener);

// Cleanup
api.DisableHooks(HookingObject.ALL);

api.Disconnect();
api.Wait(1000);

if (autoplay) {
    api.StopEditorPlay();
}
