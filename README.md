# Band.Personalize

A personalization tool for the Microsoft Band and Microsoft Band 2, built on the Universal Windows Platform.

## How to Use

1. Install the Band.Personalize application from the [Windows Store](https://www.microsoft.com/store/apps) on your preferred device (a direct link will be provided after the intitial version is published)
2. Pair your Band or Band 2 to your preferred device via Bluetooth
   1. The [Microsoft Band SDK](https://developer.microsoftband.com/bandsdk) does not support USB connections yet, as of 1.3.20628 - that functionality appears limited to the first party Microsoft Health app
   2. If you would like to see USB support, please vote for it on the Band [User Voice](https://microsofthealth.uservoice.com/forums/283636-microsoft-health-and-microsoft-band/suggestions/14103882-add-sdk-for-mac-and-windows-usb) page
3. Run the Band.Personalize application
4. Select your paired Band from a grid of paired Bands on it (in the future, this page will automatically be skipped if there is exactly one paired and connected Band)
   1. You can refresh the list of paired Bands by clicking the "refresh" icon on the application bar
5. Use the color picker tiles under "Theme" to re-theme your Band
   1. To see what parts of the Band are controlled by each color, see the [Microsoft Band Experience Design Guidelines](https://developer.microsoftband.com/content/docs/microsoftbandexperiencedesignguidelines.pdf)
   2. To see what parts of the Band 2 are controlled by each color, see the [Microsoft Band 2 Experience Design Guidelines](https://developer.microsoftband.com/Content/docs/MicrosoftBandExperienceDesignGuidelines2.pdf)
   3. Be sure to apply your changes by clicking the save icon on the application bar
   4. You can cancel your unsaved changes by clicking the "refresh" icon on the application bar
6. Use the image picker under "Image" to change the Me Tile image on your Band
   1. Be sure to apply your changes by clicking the save icon on the application bar
   2. You can cancel your unsaved changes by clicking the "refresh" icon on the application bar
7. (Optional) Re-pair your Band or Band 2 with your phone or other device
   1. You may have to delete the previous Band pairing from your phone or other device before re-pairing your Band - this appears to be a limitation of the Band

## How to Develop

1. Clone the repository
2. You must use Windows 10 for development
3. Enable Windows 10 developer mode by following these [instructions](https://msdn.microsoft.com/en-us/windows/uwp/get-started/enable-your-device-for-development)
4. Open the solution `Band.Personalize.sln` in Visual Studio
5. Generate a test certificate for your local copy:
   1. Open the `Package.appxmanifest` for the `Band.Personalize.App.Universal` and `Band.Personalize.Model.Test` projects
   2. Open the "Packaging" tab for each manifest
   3. Select "Choose Certificate…"
   4. Choose "Create test certificate…" from the "Choose Certificate…" dropdown
   5. Fill out the options for your test certificate - the defaults supply the username of the logged-in Windows account and a blank password
6. Select x86 or x64 as the build configuration unless you have a Windows 10 Mobile device to use with the ARM configuration
7. The "Debug (Stub)" configuration enables fake Band data for the application
8. The "Release (Tests)" configuration builds in release mode but still applies the required `InternalsVisibleToAttribute` for executing tests

---

Microsoft is a registered trademark of the Microsoft Corporation.  This app is **not** sponsored, developed, maintained, or supported by Microsoft.