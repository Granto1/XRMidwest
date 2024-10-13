# About  _VR Project Template_

The VR Project Template configures project settings for Unity applications that wish to use Virtual Reality.

This Template uses the following Unity features:

* Interfaces for the following platforms: Oculus and Hololens.
* XR Management:  a tool to help streamline XR plug-in lifecycle management and potentially provide users with build time UI through the Unity Unified Settings system.  For more information see the [XR Management docs](https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html).


The template contains the following:
* A sample scene that is already configured for Virtual Reality, including an `XR Rig` and visualizations for the controllers.
* Example Assets for an VR Scene all in a single folder that can be easily removed.
* Script to choose properly oriented controller models to ensure that controllers display properly regardless of which XR package is being used.


## Template CI
CI has been added to the project and it will test your template on every commit on `Yamato`.
This will validate that the template package as well as embedded packages (if any) have the right structure, have tests and do not create console logs when opened with Unity.
The CI will also automatically test the template as it would be used by a user on multiple editor versions and OS.
You might need to tweak the list of editors and OS you want to test the template on. For more information, please [go here](https://confluence.hq.unity3d.com/pages/viewpage.action?spaceKey=PAK&title=Setting+up+your+package+CI)

`Note`: To make use of the CI, your repository must be added to Yamato.
Log in to [Yamato](https://unity-ci.cds.internal.unity3d.com/) and click on the Project + button on the top right.  This will open a dialog asking for you to specify a git url and project name.

## Trying out your template locally.

If you want to test your template locally from a user's perspective, you will need to make it available to a Unity Editor. This can be accomplished in a couple of ways:

1. Use the template tgz that's built by yamato

After creating a PR, Yamato will build the template tgz file. To download it, open the [Yamato] Pack link, go to Artifacts and then click the download button to download the things built by this yamato job.

![Yamato pack link](yamatoPackLink.png)
![Yamato Artifacts Download link](yamatoArtifactsDownload.png)

Close the Unity Editor and Unity Hub. To make sure that Unity Hub is closed, go to the system tray, right click the Unity Hub icon, and then quit unity hub. (Closing the window doesn't actually close Unity Hub)
	
![Quit Unity Hub](quitUnityHub.png)

Copy the template tgz file generated by yamato and copy it into the version of Unity that you want to install this for. The filepath that I copied this into was: C:\Program Files\Unity\Hub\Editor\2021.2.8f1\Editor\Data\Resources\PackageManager\ProjectTemplates
	
![Copy Template Tgz](copyTemplateTgz.png)

Launch Unity Hub, select the version of Unity that you copied the template tgz into and then select the VR Template option. If you changed the version number in the change, the updated version number will show in Unity Hub.

![VRTemplate Selection](vrTemplateSelection.png)
![VRTemplate Version](vrTemplateVersion.png)
	

2. Use upm-ci tools to test your template

    You need to make sure you have `Node.js` and `npm` _(install it from [here](https://nodejs.org/en/))_ installed on your machine to package successfully, as the script calls `npm` under the hood for packaging and publishing. The script is tested with `node@v10.16.0` and `npm@5.6.0`.
    Install globally the upm-ci package:

    ```npm install upm-ci-utils -g --registry https://api.bintray.com/npm/unity/unity-npm```

    1. **To run all your template tests**
        1. Open a console (or terminal) window and cd your way inside your template project folder

            ```upm-ci template test -u 2018.3```

            You can test against many versions of Unity with the -u parameter:

            - Testing on a specific version: use `-u 2019.1.0a13`
            - Testing on a latest release of a version: use `-u 2019.1`
            - Testing on the latest available trunk build: use `-u trunk`
            - Testing on a specific branch: use `-u team-name/my-branch`
            - Testing on a specific revision: use `-u 3de2277bb0e6`
            - Testing with an editor installed on your machine: use `-u /absolute/path/to/the/folder/containing/Unity.app/or/Unity.exe`

            By default, this will download the desired version of the editor in a .Editor folder created in the current working directory.

    1. **To test what a user would see**
        1. Open a console (or terminal) window and cd your way inside your template project folder

            ```upm-ci template pack```
            This will generate a folder /upm-ci~/templates/ containing a .tgz file of your converted template.


        1. Include the tarballed template package in Unity editor

            You can then copy the template's `tgz` package file in Unity in one of these paths to make it available in the editor when creating new projects:

            1. Mac: `<Unity Editor Root>/Contents/Resources/PackageManager/ProjectTemplates`

            1. Windows: `<Unity Editor Root>/Data/Resources/PackageManager/ProjectTemplates`

        1. Preview your project template

            Open Unity Hub. Locate the editor to which you added your template to.
            When creating a new project, you should see your template in the templates list:

            ![Template in new project](Packages/com.unity.template.mytemplate/Documentation~/images/template_in_new_project.png)

            Note: If you are launching the Unity editor without the hub, you will not see additional templates in the list.

## Publishing your template for use in the Editor

The first step to get your package published to production for public consumption is to send it to the candidates repository, where it can be evaluated by QA and Release Management.  You can publish your template to the candidates repository through the added CI, which is the **recommended** approach.

1. Once you are ready to publish a new version, say version `1.0.0`, you can add a git tag `rc-1.0.0` to the commit you want to publish. The CI will validate and then publish your template to `candidates`.

1. Request that your template package be published to production by [filling out the following form](https://docs.google.com/forms/d/e/1FAIpQLSeEOeWszG7F5mx_VEYm8SrjcIajxa5WoLXh-yhLvw8odsEnaQ/viewform)

1. Once your template is published to production, the last step is to create the Ono PR to include your template with a Unity Release, and have it be discovered in the Hub. To do so, create a branch that includes your template in `External/PackageManager/Editor/editor_installer.json`

`Note`: You can retrieve a version of your template package as an artifact from CI pipelines following any commit made to your repository.  This will allow you to easily test a change at any point during your development.