# WebPack Fundamentals - PluralSight course

Why we need a build process ? :

## Multiple web requests
hundreds of js files = hundreds of requests
so combine them

## Code size
compression, minification for js and css

## File Order Dependencies
e.g. order of loading js files which depend on each other

Use module system to track order, reduce maintenance of js

## Transpilation
For browsers which dont handle the lates version of ECMAScript

## Linting
Find bugs in js / ts...

---

Other Solutions, aside from webpack : asp.net bundling, task runners such as grunt and gulp which suppoort more things such as running unit tests, but need configuring. Browserify for module management.

Webpack is more specialised than above to translate input files into output files, using loaders.

Webpack works with NPM not bower.

Webpack requires you to use a module system.

---

## Module Systems
A graph of JS files which have various dependances on each other.

There are three main module systems, looking at CommonJS, which is what Treaties seems to use ???

```js
  var $ = require('jQuery'); // from npm so only name required , not path
  var members = require('./Members'); // one of ours so specify the path, i.e. the .
```

In CommonJS, modules are loaded synchronously, and processed in the order the JavaScript runtime finds them. This system was born with server-side JavaScript in mind, and is not suitable for the client-side (this is why ES Modules were introduced).

ECMAScript module syntax looks like ths :

```js
  import {login} from "./login"
```

---

[]install webpack(https://webpack.js.org/guides/installation/)

```
  npm install webpack -g
  npm install webpack-cli -g
```

global install not actuall recomended ?

## CLI

```
  webpack development
  webpack ./app.js
```

This builds dist\main.js
now point our markup at main.js, src attribute.

## Config Files
Once you have setup a config file, you can do build by simply running the webpack command.

Create new file webpack.config.js :
```
module.exports = {
  entry: "./app.js",
  output: { filename: "bundle.js" }
}
```

now run webpack, now we get dist\bundle.js

## Watchmode
```
webpack --watch
```

or edit config :
```
module.exports = {
  entry: "./app.js",
  output: { filename: "bundle.js" },
  watch: true
}

```

## Dev Server
Webpacks own webserver
```
  npm install webpack-dev-server -g
```

then just run : webpack-dev-server

## More than one JS file
Add a require<filename> to app.js ( or whatever file is entry in webpack.config.js )

or change entry to be an array in that same config file above.

## Loaders ( Extensions )
Needed if you want to do more than process js files ( combine, minify .. )

e.g. babel can change files from ECMAScript 6 to 5. so you can author JS in ECMAScript 6

see "devDependencies" in package.json, heres the wevpack.common.js for Treaties, showing modules setting for babel.

```js
const path = require('path');
const webpack = require('webpack');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    entry: {
        app: './app/main.ts'
    },
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: '[name].bundle.js',
        publicPath: '/dist/'
    },
    plugins: [
        new CleanWebpackPlugin(),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            'window.jquery': 'jquery'
        }),
        new ForkTsCheckerWebpackPlugin(),
        new CopyWebpackPlugin([
            // Add any other static files you need here
            { from: './app/img/favicon.ico' },
            { from: './app/img/opengraph-card.png' },
            { from: './app/img/treaties-illustration.svg' }
        ])
    ],
    resolve: {
        extensions: ['.ts', '.js']
    },
    module: {
        rules: [
            {
                test: /\.js?$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader'
                }
            },
            {
                test: /\.(png|svg|jpg|gif|ttf|woff|woff2|eot)$/,
                use: {
                    loader: 'url-loader',
                    options: {
                        limit: 30000,
                        name: "[name].[ext]"
                    }
                }
            }
        ]
    }
};
```

## Production Builds

minify code : webpack -p

npm --save-dev <some-package> // writes into package.json file with correct version

you can see from this webpack.production.config.js itg merges in the common file as a starting point.


```js
const merge = require('webpack-merge');
const common = require('./webpack.common.config');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');

module.exports = merge(common,
    {
        mode: 'production',
        devtool: 'source-map',
        optimization: {
            minimizer: [
                new UglifyJsPlugin({
                    cache: true,
                    parallel: true,
                    sourceMap: true
                }),
                new OptimizeCssAssetsPlugin()
            ]
        },
        plugins: [
            new MiniCssExtractPlugin({
                filename: '[name].css',
                chunkFilename: '[id].css'
            })
        ],
        module: {
            rules: [
                {
                    test: /\.ts?$/,
                    exclude: /node_modules/,
                    use: [
                        {
                            loader: 'babel-loader'
                        },
                        {
                            loader: 'ts-loader',
                            options: {
                                transpileOnly: false
                            }
                        }
                    ]
                },
                {
                    test: /\.s?[ac]ss$/,
                    use: [
                        {
                            loader: MiniCssExtractPlugin.loader
                        },
                        {
                            loader: 'css-loader',
                            options: {
                                importLoaders: 1,
                            }
                        },
                        {
                            loader: 'postcss-loader'
                        },
                        {
                            loader: 'sass-loader'
                        }
                    ]
                }
            ]
        }
    }
);
```
run with : webpack --config webpack-production.config.js -p

## Sourcemaps
just add -d to webpack call

so that chrome debug tools shows JS code, not the minified/uglified version

