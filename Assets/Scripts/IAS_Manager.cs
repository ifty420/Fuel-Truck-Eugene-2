// i6 IAS_Manager.cs [Updated 5th January 2015]
// Attach this script to a persistent Game Object
// Contact sean@i6.com for help and information

/* Change Log:
 * 23rd December 2014:
 * - Added texture caching per session, the script will now check the timestamp on the URL and if it's asked to download the same texture again
 * it will compare it to the last timestamp used for that texture, if it's a match then a stored version of the texture is re-used instead
 * of re-downloading the texture each time it needs it.
 * - General cleanup and line commenting
 *
 * 5th January 2015:
 * - IAS script will now safely be stopped instead of throwing an error when it fails to connect to ias.i6.com
 * - Safe exit added to image downloads when the network connection is lost during an image download instead of throwing an error
 * - Optional GoogleAnalytics commented out lines have been added! Search for GoogleAnalytics.Instance.LogError and uncomment those lines if you wish to log the IAS errors to GoogleAnalytics
 */

using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

// Storage class for the main IAS advert IDs for selection
[System.Serializable]
public class IAS_Grouping
{
	public int ScreenID { get; set; }
	
	private List<string> _SlotIDs = new List<string>();
	public List<string> SlotIDs
	{
		get { return _SlotIDs; }
		set { _SlotIDs = value; }
	}
}

public class IAS_Manager : MonoBehaviour
{      
	public static IAS_Manager Instance;
	
	// Note: You should probably replace references to BundleID with a reference to your bundle ID from another script
	public string BundleID = "com.i6.GameNameHere";
	
	// Dictionaries used for storing cache data (See the NeedToDownload function)
	private Dictionary<string, int> CachedURLs = new Dictionary<string, int>();
	private Dictionary<string, Texture> CachedTextures = new Dictionary<string, Texture>();
	
	// Main IAS variables (Set your IAS Ad URL from the inspector of the object which this script is attached to)
	private bool _Main_IASReady = false;
	public bool Main_IASReady
	{
		get { return _Main_IASReady; }
		private set { _Main_IASReady = value; }
	}
	
	public List<int> includedScreenIDs = new List<int>();
	
	public string IAS_AdURL = "http://ias.i6.com/ad/30.json";
	private List<string> Main_BannerURLs = new List<string>();
	private List<string> Main_BannerImageURLs = new List<string>();
	private List<Texture> Main_BannerTextures = new List<Texture>();
	
	// Backscreen IAS variables (You don't need to change these variables)
	private bool _Backscreen_IASReady = false;
	public bool Backscreen_IASReady
	{
		get { return _Backscreen_IASReady; }
		private set { _Backscreen_IASReady = value; }
	}
	
	private int Backscreen_AdLimit = 3;
	private string IAS_Static_Ads = "http://ias.i6.com/ad/30.json";
	private List<string> Backscreen_BannerURLs = new List<string>();
	private List<string> Backscreen_BannerImageURLs = new List<string>();
	private List<Texture> Backscreen_BannerTextures = new List<Texture>();
	
	void Awake()
	{
		if(!Instance)
			Instance = this;
	}
	
	void Start()
	{
		// Fetch the main banners
		StartCoroutine(FetchMainBanners());
		
		// Fetch the Backscreen banners
		StartCoroutine(FetchBackscreenBanners());
	}
	
	public void ResetBackscreenBanners()
	{
		if(!Backscreen_IASReady)
			return;
		
		// Mark the Backscreen IAS ads as not ready whilst re-downloading the data
		Backscreen_IASReady = false;
		
		// Clear the Backscreen IAS lists
		Backscreen_BannerURLs.Clear();
		Backscreen_BannerImageURLs.Clear();
		Backscreen_BannerTextures.Clear();
		
		// Fetch the Backscreen banners
		StartCoroutine(FetchBackscreenBanners());
	}
	
	public void ResetMainBanners()
	{
		if(!Main_IASReady)
			return;
		
		// Mark the main IAS ads as not ready whilst re-downloading the data
		Main_IASReady = false;
		
		// Clear the main IAS lists
		Main_BannerURLs.Clear ();
		Main_BannerImageURLs.Clear ();
		Main_BannerTextures.Clear ();
		
		// Fetch the main banners
		StartCoroutine(FetchMainBanners());
	}
	
	public string GetAdURL(int bannerIndex, bool isBackscreen = false)
	{
		return (!isBackscreen ? Main_BannerURLs[bannerIndex - 1] : Backscreen_BannerURLs[bannerIndex - 1]);
	}
	
	public Texture GetAdTexture(int bannerIndex, bool isBackscreen = false)
	{
		return (!isBackscreen ? Main_BannerTextures[bannerIndex - 1] : Backscreen_BannerTextures[bannerIndex - 1]);
	}
	
	private void SetCachedTexture(string URL, int TimeStamp, Texture DLTexture)
	{
		// Update the CachedTextures dictionary with the newly downloaded texture
		CachedTextures[URL] = DLTexture;
		
		// Update the CachedURLs dictionary for the new texture
		CachedURLs[URL] = TimeStamp;
	}
	
	private bool NeedToDownload(string URL, int TimeStamp)
	{
		if(CachedURLs.ContainsKey(URL)){
			if(CachedURLs.ContainsValue(TimeStamp)){
				// We have the URL cached and it's upto date
				return false;
			} else {
				// We have the URL cached, however it's outdated
				return true;
			}
		} else {
			// We don't have the URL cached at all
			return true;
		}
	}
	
	private IEnumerator FetchBackscreenBanners()
	{
		// Request JSON data from the URL (We read this URL everytime)
		WWW wwwJSON = new WWW(IAS_Static_Ads);
		
		// Wait for the JSON data to be collected from the URL
		yield return wwwJSON;
		
		if (!string.IsNullOrEmpty(wwwJSON.error)){
			GoogleAnalytics.Instance.LogError("Preclose IAS Download error: " + wwwJSON.error, false);
			Debug.LogWarning("Preclose IAS Download error: " + wwwJSON.error);
			return false;
		}
		
		// Parse the JSON data we just read into usable data
		JSONNode rootNode = JSON.Parse (wwwJSON.text);
		
		// Loop for each "slot" in the JSON data
		foreach(JSONNode node in rootNode["slots"].AsArray)
		{
			// We need to work with the nodes as strings so just set them to local variables
			string sloturl = node["adurl"];
			string slotimg = node["imgurl"];
			
			// Break out of the foreach if we hit the max banners needed
			if(Backscreen_BannerURLs.Count >= Backscreen_AdLimit)
				break;
			
			// If the current ad is an advert for this game then skip it
			// Note: You should probably replace BundleID with a reference to your bundle ID from another script
			if(sloturl.Contains (BundleID))
				continue;
			
			// Add the advert URL as an item to the list
			Backscreen_BannerURLs.Add(sloturl);
			
			// Set the ad image URL to a new item in the list
			Backscreen_BannerImageURLs.Add(slotimg);
		}
		
		// Dispose of the JSON data
		wwwJSON.Dispose();
		
		// Download the images
		for(int i = 0; i < Backscreen_BannerURLs.Count; i++)
		{
			// Split the URL into parts using ? as a delimiter
			// We need to set this to an array variable so we can check the length before doing anything with it or it'll break the whole script
			string[] URLParts = Backscreen_BannerImageURLs[i].Split("?".ToCharArray(), System.StringSplitOptions.None);
			
			// Create an int for the timestamp (Separate due to use of TryParse)
			int IASTimeStamp = 0;
			
			// Hopefully if we're in the future and the IAS was changed then not too much was changed about it
			// If there is atleast 2 parts in the URL then take the second part and hope it's the timestamp
			// Incase it's not and Don changed everything then TryParse will always default to 0 and not throw any errors)
			// Plus if in the future the timestamp was removed from the URL completely then we'll just use 0 as the actual value which effectively caches everything this session
			int.TryParse(URLParts.Length >= 2 ? URLParts[1] : "0", out IASTimeStamp);
			
			// Store the downloaded texture in this variable once downloaded or loaded from the cache
			Texture ReadyIASTexture;
			
			// Check if we need to download this IAS banner or if we already have an upto date version stored in the CachedTextures dictionary
			if(NeedToDownload(URLParts[0], IASTimeStamp)){
				
				// Request the banner texture from the full URL including timestamp
				WWW wwwImage = new WWW(Backscreen_BannerImageURLs[i]);
				
				// Wait for the image data to be downloaded
				yield return wwwImage;
				
				if (!string.IsNullOrEmpty(wwwImage.error)){
					GoogleAnalytics.Instance.LogError("Preclose IAS Image Download error: " + wwwImage.error, false);
					Debug.LogWarning("Preclose IAS Image Download error: " + wwwImage.error);
					return false;
				}
				
				// Set ReadyIASTexture to the newly downloaded texture
				ReadyIASTexture = wwwImage.texture;
				
				// Update the cache dictionaries
				SetCachedTexture(URLParts[0], IASTimeStamp, ReadyIASTexture);
				
				// Dispose of the downloaded data, we don't need it anymore
				wwwImage.Dispose();
				
			} else {
				
				// Set the ReadyIASTexture to the texture in the CachedTexture dictionary
				ReadyIASTexture = CachedTextures[URLParts[0]];
				
			}
			
			// Set the texture
			Backscreen_BannerTextures.Add(ReadyIASTexture);
			
		}
		
		Backscreen_IASReady = true;
	}
	
	private IEnumerator FetchMainBanners()
	{
		// Request JSON data from the URL
		WWW wwwJSON = new WWW(IAS_AdURL);
		
		// Wait for the JSON data to be collected from the URL
		yield return wwwJSON;
		
		if (!string.IsNullOrEmpty(wwwJSON.error)){
			GoogleAnalytics.Instance.LogError("Main IAS Download error: " + wwwJSON.error, false);
			Debug.LogWarning("Main IAS Download error: " + wwwJSON.error);
			return false;
		}
		
		// Parse the JSON data we just read into usable data
		JSONNode rootNode = JSON.Parse (wwwJSON.text);
		
		// This method of sorting the JSON data isn't the cleanest but it works
		// We need to order the JSON data starting from screen 1 and counting up
		// Let us know if you can improve this code block
		// Note: We would prefer not to use and heavy sorting methods as performance is important
		string sortedJSON = "";
		
		// Wrap the sorted JSON within the slots array item
		sortedJSON += "{\"slots\":[";
		
		for(int i = rootNode["slots"].Count;i >= 0;i--)
		{
			if(rootNode["slots"].AsArray[i] != null)
			{
				sortedJSON += (rootNode["slots"].AsArray[i].ToString());
			}
		}
		
		// Close the slots wrapper
		sortedJSON += "]}";
		
		// Replace the rootNode with the new sortedJSON data
		rootNode = JSON.Parse (sortedJSON);
		
		// IAS_Grouping is a custom class defined at the top of this script
		// The class returns the values ScreenID (int) and SlotIDs (List<string>)
		List<IAS_Grouping> IAS_SlotGroups = new List<IAS_Grouping>();
		
		// Local int to store the previous slot value so we can compare if it has changed each iteration
		int prevSlotVal = 0;
		
		// List used to store the generated random slotIDs based from the available slots
		List<int> curSlotID = new List<int>();
		
		// Iterate through all slots from the JSON data
		foreach(JSONNode node in rootNode["slots"].AsArray)
		{
			string slotID = node["slotid"];
			string slotURL = node["adurl"];
			string slotChar = slotID[slotID.Length-1].ToString();
			int slotVal = int.Parse(slotID[slotID.Length-2].ToString());
			
			// Check if we have iterated onto a new screen ID
			if(slotVal != prevSlotVal)
			{
				// Add the IAS Grouping class item to the list as a new item
				IAS_SlotGroups.Add (new IAS_Grouping());
				
				// Set the stored screen ID for this list item and set the previous slot value too
				IAS_SlotGroups[slotVal-1].ScreenID = slotVal;
				
				// Set the previous slot value so we don't add this screen ID again
				prevSlotVal = slotVal;
			}
			
			// Skip any adverts which advertise its self and display the next ad instead
			if(slotURL.Contains (BundleID))
				continue;
			
			// Set the current slot ID inside the current screen ID list
			// Why is this not working ?!?
			IAS_SlotGroups[slotVal-1].SlotIDs.Add(slotChar);
		}
		
		// Ensure all the screen IDs have atleast 1 advert each
		foreach(IAS_Grouping screenSlotIDs in IAS_SlotGroups)
		{
			if(screenSlotIDs.SlotIDs.Count <= 0)
			{
				Debug.Log ("IAS Screen ID " + screenSlotIDs.ScreenID + " does not have any ad slots!");
			}
		}
		
		int curSlotCount = 0;
		prevSlotVal = 0;
		
		// Loop for each "slot" in the JSON data
		foreach(JSONNode node in rootNode["slots"].AsArray)
		{
			// We need to work with the nodes as strings so assign them to local variables
			string slotURL = node["adurl"];
			string slotIMG = node["imgurl"];
			string slotID = node["slotid"];
			int screenSlot = int.Parse(slotID[slotID.Length-2].ToString());
			
			// Skip any adverts which advertise its self and display the next ad instead
			if(slotURL.Contains (BundleID))
				continue;
			
			if(screenSlot != prevSlotVal){
				// Reset the slot count because we've moved to the next screen ID
				curSlotCount = 0;
				
				// Load information about the current slot id
				curSlotID.Add(PlayerPrefs.GetInt("IAS_ADSlot_" + (screenSlot - 1), 0));
				
				// Increase the curSlotID if it's lower than the max, else reset it to advert slot 1
				if(curSlotID[screenSlot - 1] + 1 < IAS_SlotGroups[screenSlot-1].SlotIDs.Count){
					// Increase the slot ID for the current screen
					curSlotID[screenSlot - 1]++;
				} else {
					// Set the slot ID for the current screen back to 1
					curSlotID[screenSlot - 1] = 0;
				}
				
				PlayerPrefs.SetInt("IAS_ADSlot_" + (screenSlot - 1), curSlotID[screenSlot - 1]);
				
				prevSlotVal = screenSlot;
			} else {
				// Increase the cur slot count
				curSlotCount++;
			}
			
			// Does the current slotID iteration match the randomly generated slotID for this screenID?
			if(curSlotCount == curSlotID[screenSlot - 1])
			{
				// Add the advert URL as an item to the list
				Main_BannerURLs.Add (slotURL);
				
				// Set the ad image URL to a new item in the list
				Main_BannerImageURLs.Add (slotIMG);                            
			}
		}
		
		// Dipose of the JSON data
		wwwJSON.Dispose ();
		
		// Download the images
		for(int i = 0; i < Main_BannerURLs.Count; i++)
		{
			
			Main_BannerTextures.Add (new Texture());
			
			// Limit the screen IDs being used for this game
			if(!includedScreenIDs.Contains(i+1) && includedScreenIDs.Count > 0)
				continue;
			
			// Split the URL into parts using ? as a delimiter
			// We need to set this to an array variable so we can check the length before doing anything with it or it'll break the whole script
			string[] URLParts = Main_BannerImageURLs[i].Split("?".ToCharArray(), System.StringSplitOptions.None);
			
			// Create an int for the timestamp (Separate due to use of TryParse)
			int IASTimeStamp = 0;
			
			// Hopefully if we're in the future and the IAS was changed then not too much was changed about it
			// If there is atleast 2 parts in the URL then take the second part and hope it's the timestamp
			// Incase it's not and Don changed everything then TryParse will always default to 0 and not throw any errors)
			// Plus if in the future the timestamp was removed from the URL completely then we'll just use 0 as the actual value which effectively caches everything this session
			int.TryParse(URLParts.Length >= 2 ? URLParts[1] : "0", out IASTimeStamp);
			
			// Store the downloaded texture in this variable once downloaded or loaded from the cache
			Texture ReadyIASTexture;
			
			// Check if we need to download this IAS banner or if we already have an upto date version stored in the CachedTextures dictionary
			if(NeedToDownload(URLParts[0], IASTimeStamp)){
				
				// Request the banner texture from the full URL including timestamp
				WWW wwwImage = new WWW(Main_BannerImageURLs[i]);
				
				// Wait for the image data to be downloaded
				yield return wwwImage;
				
				if (!string.IsNullOrEmpty(wwwImage.error)){
					GoogleAnalytics.Instance.LogError("Main IAS Image Download error: " + wwwImage.error, false);
					Debug.LogWarning("Main IAS Image Download error: " + wwwImage.error);
					return false;
				}
				
				// Set the ReadyIASTexture to the newly downloaded texture
				ReadyIASTexture = wwwImage.texture;
				
				// Update the cache dictionaries
				SetCachedTexture(URLParts[0], IASTimeStamp, ReadyIASTexture);
				
				// Dispose of the downloaded data, we don't need it anymore
				wwwImage.Dispose();
				
			} else {
				
				// Set the ReadyIASTexture to the texture in the CachedTexture dictionary
				ReadyIASTexture = CachedTextures[URLParts[0]];
				
			}
			
			// Add the texture to the list
			Main_BannerTextures[i] = (ReadyIASTexture);
			
		}
		
		// Set the IASReady variable to true once all the ads are ready
		Main_IASReady = true;

		if (MenuAI.instanse != null)
		{
			MenuAI.instanse.loadBanner();
		}
	}
}
