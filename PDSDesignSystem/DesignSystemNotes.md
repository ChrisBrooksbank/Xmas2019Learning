# Design System Notes

Style, UI component and pattern library for UK Parliament. Use this design system to make your applications consistent with others across Parliament.

[Design system documentation](https://designsystem.digiminster.com/)

[Bootstrap documentation](https://getbootstrap.com/)

[Create new .Net Core solution](https://hopuk.sharepoint.com/sites/BCT-BusSys/SitePages/Creating%20a%20.Net%20Core%20Solution%20using%20a%20custom%20Template.aspx)

## Getting Started
[Getting started](https://designsystem.digiminster.com/getting-started/)

The PDS Design System is essentially an NPM package that contains all the SASS files packaged up allowing you to utilise the UK Parliament styles and components.

[NPM digiminster packages](https://npm.digiminster.com/#/)

```
npm set registry https://npm.digiminster.com
```

## Fundamentals
### Layout
    Banner area, header, main content area ( containing other components), footer

bolerplate HTML

```html
<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link href="~/dist/favicon.ico" rel="shortcut icon" type="image/x-icon">

        <title>[Website name] - UK Parliament</title>

        <meta name="description" content="[Page description]">

        <!-- Opengraph area -->

        <!-- Tracking script area -->
        
        <!-- CSS area -->
    </head>

    <body class="brand-standard">
        <!-- Skip to content area -->
        
        <!-- Script banner area -->
        
        <!-- Cookie banner area -->
        
        <!-- Header area -->

        <main id="main-content">
            <!-- Product banner area -->

            <div class="container">
                <article>
                    <!-- Main content area -->
                </article>
            </div>
        </main>

        <!-- Footer area -->

        <!-- Script area -->
    </body>
</html>
```

## Styles

### Branding
https://designsystem.digiminster.com/styles/branding/

There are 4 'brands' in the design system:

* General: neutral brand, which is not specific to any house at all.
* House of Commons: specific to the House of Commons.
* House of Lords: specific to the House of Lords.
* Bicameral/joint: used where joint houses/bicameral need to be represented.

```html
<body class="brand-general">
<body class="brand-commons">
<body class="brand-lords">
<body class="brand-bicameral">
```

There are also utility classes which will set various colours based on which brand is set. ""

```html
<div class="brand-background-colour">
    This is the brand background colour for bicameral/joint
</div>

<div class="brand-colour">
    This is the brand colour for bicameral/joint
</div>

<div class="brand-border-colour">
    This is the brand border colour for bicameral/joint
</div>
```


### Colours
[Documentation](https://designsystem.digiminster.com/styles/colour)
shows actual colours used, sass variable names, hex codes

### Favicon
[Documentation](https://designsystem.digiminster.com/styles/favicon/)

### Illustrations
[Documentation](https://designsystem.digiminster.com/styles/illustrations) WIP 

### Print stylesheets
[Documentation](https://designsystem.digiminster.com/styles/print/)

### Typography
[Documentation](https://designsystem.digiminster.com/styles/typography/)

The UK Parliament brand typeface is National, and this should be used for any UK Parliament websites/applications.

It is available in the following webfont formats:

* EOT
* WOFF
* WOFF2

The design system has these fonts setup and ready to use.

If, for aesthetic reasons, you wanted to use a specific heading tag, but wanted to make it appear larger or smaller, then you can apply the heading-level-# override classes to them. For example, to make a h3 look like a h1, you can apply the class heading-level-1.

```html
<h1>Heading level 1</h1>
<p class="sub-heading">This is a sub-heading for heading</p>
<p>This is the rest of the page content.</p>
```

You should only use the mixins as they set both the font size and line-height, which have been designed to work together. Setting only the font size or line height separately may result in rendering/readability issues.

When rendering large blocks of text, it is generally considered good practice to restrict them to a maximum width at large screen sizes. The reading-width class can be applied to elements that contain large amounts of text.

It will automatically restrict the width at larger screen sizes, but remain full width at smaller screen sizes.

There is a helper for making text bold. Include the SASS helper font-bold, rather than setting the font-weight manually to ensure bold is applied in a consistent way.

```sass
.my-selector {
    @include font-bold;
}
```

## Opengraph
[Documentation](https://designsystem.digiminster.com/guidelines/opengraph) WIP

## SEO guidelines
[Documentation](https://designsystem.digiminster.com/guidelines/seo/) ( WIP ) 

## Components
* Card

    Anywhere where lists of items or signifcant pieces of information are shown, the card component is used to display the details. For example, we use cards to represent Members, committees, divisions, written questions, and so forth. But they can be applied to much, much more.

    The basic card contains 2 main areas: content area ( main area ) and info area ( at bottom )

        Content area: used to display the most important information about what you are showing, or information that is long and takes up a lot of space. For example, the title of what you are showing. Or a description.

        Info area: the info area is for supplementary information, which is either less important, or is short enough that it can fit in the info area. For example, the ID number of what you are showing, the date it was created, or a way of indicating a status (indicators are explained below).

        ```html
        <a href="#" class="card">
    <div class="card-inner">
        <div class="content">
            <div class="primary-info">
                This is the primary info
            </div>
            <div class="secondary-info">
                This is the secondary info
            </div>
                <div class="tertiary-info">
                This is the tertiary info
            </div>
        </div>
        <div class="info">
            <div class="info-inner">
                <div class="indicators-left">
                    <div class="indicator indicator-label">Label indicator</div>
                </div>
                <div class="indicators-right">
                    <div class="indicator">
                        <span class="icon fas fa-user" aria-hidden="true"></span>
                        Icon indicator
                    </div>
                </div>
            </div>
        </div>
    </div>
</a>
        ```

        The text block is used when there is a significant block of text which needs to be displayed, which is too large to show in the .#-info blocks.

        The list block is used when you need to shown some label: value information. It supports multiple items, and also multiple values.

        e.g.

        ```html
        <a href="#" class="card">
    <div class="card-inner">
        <div class="content">
            <div class="primary-info">
                This is the primary info
            </div>
            <div class="list">
                <div class="item">
                    <span class="label">Label</span>
                    Value 1, Value 2, Value 3
                </div>
                <div class="item">
                    <span class="label">Another label</span>
                    Another value
                </div>
            </div>
        </div>
    </div>
</a>
        ```

        The infographic block is used when you need to show numeric or statistical values in the card. It supports single, large values and multiple, small values (or a combination of both).

        There are two parts to an info area - the .indicators-left side, and .indicators-right side. 

        Icon indicators are designed to be used with Font Awesome.

        There is also a special indicator that can be used to represent either the Commons or the Lords houses using the .indicator-house and indicator-house-commons/.indicator-house-lords classes.

        You can use the .card-light class to indicate a disabled state, or another form of secondary state.

        You can use the .card-small class to render a more compact version of the card where there may be limited space

* House card
    The house card is used to present information that has a prominent association with a house.

    It is essentially the same as a standard card, but features the house colour with a portcullis icon on. It supports the same types of content as the standard card.

    ```html
    <a href="#" class="card card-house card-house-commons">
    <div class="card-inner">
        <div class="badge-outer">
            <div class="badge-inner">
                <span class="icon" aria-hidden="true"></span>
            </div>
        </div>
        <div class="content">
            <div class="primary-info">
                Commons
            </div>
            <div class="secondary-info">
                Commons house card
            </div>
        </div>
        <div class="info">
            <div class="info-inner">
                <div class="indicators-left">
                    <div class="indicator indicator-label">Label indicator</div>
                </div>
            </div>
        </div>
    </div>
</a>
    ```

* Member card
https://designsystem.digiminster.com/components/cards/card-member/
It is one of the few cards where the format is fixed, and there isn't much flexibility with where information goes. WIP.
    
* Party card
https://designsystem.digiminster.com/components/cards/card-party/ WIP

* Card list
https://designsystem.digiminster.com/components/cards/card-list/ WIP

* Cookie banner

The cookie banner is used to inform users that cookies are used on the page, and also to gain consent for them.

Some sort of JavaScript or server side code is required to show/hide the banner. If you are using the Digiminster .NET Core website template, the component is built in (you can see Typescript component if you have access). Otherwise you may have to create your own.

```html
<div id="cookie-banner" class="attention-banner attention-banner-dismissable">
    <div class="container">
        <a href="https://www.parliament.uk/site-information/privacy/">Find out more</a> about how we use cookies. Otherwise by continuing to use the site you agree to the use of the cookies as they are currently set.
        <a href="#" class="dismiss-button">I agree</a>        
    </div>
</div>
```

* Footer
    The footer component is intended to provide a uniform footer layout and appearance across Parliamentary websites, across different display sizes. It features the copyright date and a link to the accessibility statement.

    ```html
    <footer>
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-no-spacing primary">
                &copy; UK Parliament [Year]
            </div>
            <div class="col-md-6 secondary">                    
                <a href="#">Accessibility statement</a>
            </div>
        </div>
    </div>
</footer>
    ```

* Grid
https://designsystem.digiminster.com/components/grid/ WIP

* Header
https://designsystem.digiminster.com/components/header/

The header component is intended to provide a uniform header layout and appearance across Parliamentary websites, across different display sizes. It features a basic header, along with a specific product header to identify to the user where they are.

The product header is intended to be used more commonly, and features the product name identifying the name of the website/section the user is in. It features a link to the home page of the website/section.

* Hero banner
https://designsystem.digiminster.com/components/banners/hero-banner/

The hero banner is used to summarise important content on a page, where relevant. It generally exists on the most significant detail pages on a website.

Hero banners appear directly under the header.

```html
</header>
<div class="hero-banner">
    <div class="container">
        <h1>Page title</h1>
        <h2>Sub-title</h2>
        <div class="reading-width">
            <p>
                This is explanatory text for the page. It should be around 3-4 lines in length, giving a very basic summary/synposis of what is being shown on the page. If it exceeds this length, it may need to be displayed somewhere else on the page.
            </p>
            <p>
                <a href="#">Find out more about this item</a>
            </p>
        </div>
    </div>
</div>
```

Heroes automatically respect branding. 

* Highlight
* Product banner
    The product banner is used as the primary branding for a website or section. It generally exists on the landing page, and provides introductory information for the website.

* Script banner
  inform users that JavaScript is required to be enabled to have the best experience on the website.

  It is not dismissable, and is intended to be shown at the top of the page in a <noscript> tag.

```html
<div class="attention-banner">
    <div class="container">
        You appear to have JavaScript disabled in your browser settings. You may find some parts of this website
        do not work properly without it enabled.
    </div>
</div>
```

extending : Whilst the hero banner is used 'as is', it is a component that is extremely flexible, and various website specific versions exist. For example, there is a Member hero used on the Members website with MPs and Lords photos.

The grid can be used to lay out content within it flexibly.


* Skip to content
  Press F5 and then TAP, the green "Skip to main content" appears

```html
  <a href="#main-content" class="skip-to-content">Skip to main content</a>
```

It should always be the first element inside the <body> tag. Its ID should match up with the container around the main content.

* Tile ( WIP )
* Widget ( WIP )
    Widget pages are used on landing pages, or on pages that display a variety of content with various actions. They utilise widget components and the grid to layout sections in white boxes.

## Patterns
https://designsystem.digiminster.com/patterns/
WIP

## Guidelines and checklists
https://designsystem.digiminster.com/guidelines/
WIP