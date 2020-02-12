# SAPBlazorAlert

## Full Css + Blazor Library for creating alert like SweetAlert without any js file.

Demo : 

[![Demo CountPages alpha](https://github.com/codesoroush/SAPBlazorAlert/blob/master/SAPBlazorAlertDemo/wwwroot/demo.gif?raw=true)](https://github.com/codesoroush/SAPBlazorAlert/blob/master/SAPBlazorAlertDemo/wwwroot/demo.gif?raw=true)


###### Usage
1. Add SAPBlazorAlert To Your Project
```
PM> Install-Package SAPBlazorAlert -Version 1.9.0
```

2. Add stylesheets either in your _host.cshtml (server-side blazor) or index.html (client-side blazor)
```
<link href="_content/SAPBlazorAnimate/animate.min.css" rel="stylesheet" />
<link href="_content/SAPBlazorAlert/style.css" rel="stylesheet" />

```

#### TIP : by adding this component , you will install another component that u can animate elements easy. More info at end of article.


3. Add This Line in _imports.razor
```
@using SAPBlazorAlert
@using SAPBlazorAnimate

```
<hr />

###### Let's Create an alert :

```Razor
<button class="btn btn-info" @onclick="() => this.Alert1.ShowAlert()">
    show alert 1
</button>

<SAPAlertBox @ref="Alert1"
             AlertBoxSize="SAPAlertBoxSize.Small"
             FooterText="created by SOROUSH ASADI"
             SAPAlertType="@SAPAlertType.Info"
             AnimationType="@AnimateBook.ZoomEntrances.ZoomIn.Fast()"
             Title="Normal Alert">
    <p>

        you can set your alert body here ,
        you can add buttons , or tables and any data u want.

        below you can add any action and event u want
    </p>
    <button @onclick="() => this.Alert1.HideAlert()" class="btn btn-info">
        Ok , i got it
    </button>

</SAPAlertBox>
```

###### Define a paremeter in code section for alert show control

```Razor
@code{

    public SAPAlertBox Alert1;



}
```

###### what is AnimationType  ?

you can set an animation for your alert reveal , for access to animation just use "@AnimationBook" static class.
you can pass animation reveal speed just after animation name.
if yout didnt select speed , it weill be at default.



###### Alert Types

```Razor
None (doesnt show any image)
Info
Success
Danger
Warning
Custom ( you can set your img or svg on  CustomAlertTypeImage="")
```

<hr />

### status


you can use Toast by this component 

in your page register this element for toast container


```Razor
<SAPToastArea 
              
              @ref="ToastArea"
              ToastAreatPosition="@SAPToastAreatPosition.Right"
              SAPToastDirection="SAPToastDirection.Column"
              AnimationType="@AnimateBook.Specials.JackInTheBox"
             >

</SAPToastArea>
```
###### Define a paremeter in code section for toast show control

```Razor
@code{

    public SAPToastArea ToastArea;


}
```
###### To create toast use this method

```Razor
@code{

    public SAPToastArea ToastArea;

    void CreateToast()
    {
        var toast = new SAPToast
        {
            Title = "sample text for toast , close after 10 sec",
            AlertType = SAPAlertType.Info,
            AutoCloseDelay= TimeSpan.FromSeconds(10)
        };

        ToastArea.CreateToast(toast);
    }

}
```






### Related projects


this project used [SAPBlazorAnimate (animate.css blazor version)](https://github.com/codesoroush/SAPBlazorAnimate)
