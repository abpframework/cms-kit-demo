# CMS Kit Demo

This is a sample application designed to demonstrate the capabilities of the [ABP](https://github.com/abpframework/abp)'s [CMS Kit Module](https://abp.io/docs/latest/modules/cms-kit/index).

## Live Demo

You can see the [live demo here](https://cms-kit-demo.abpdemo.com/).

## ABP Community Talks

We've organized a community talk to show CMS Kit's capabilities and used this solution for demonstration. You can watch the community talk from here: https://www.youtube.com/watch?v=S9__Hnu29tI

## Requirements

* .NET 9.0+

## How to run?

Before running the application, you should run the following command in the `CmsKitDemo` folder to install all NPM packages for the application:

```bash
abp install-libs
```

After installing the NPM packages, you can directly run the `CmsKitDemo` project to see the application. This application uses SQLite as the database and the database can be found in the solution folder. Therefore, you don't need to create the database manually.

> Default credentials: `admin` as username and `1q2w3E*` as the password.

## Screenshots

### Homepage

The application menu items (gallery, blog, ...) are created & ordered dynamically with [CMS Kit's Menu System](https://abp.io/docs/latest/Modules/Cms-Kit/Menus):

![homepage](assets/images/homepage.png)

### Image Gallery

Custom implementation (image gallery) with CMS Kit’s [Comment](https://abp.io/docs/latest/Modules/Cms-Kit/Comments) & [Reaction](https://abp.io/docs/latest/Modules/Cms-Kit/Reactions) Features integrated:

| Gallery | Detail Page  |
|------------------------ |-----------------------|
| ![](assets/images/image-gallery.jpg) | ![](assets/images/image-gallery-detail.jpg)  |

### Blog & Blog Posts

[Blogging Feature](https://abp.io/docs/latest/Modules/Cms-Kit/Blogging) (with [Ratings](https://abp.io/docs/latest/Modules/Cms-Kit/Ratings), [Comments](https://abp.io/docs/latest/Modules/Cms-Kit/Comments), [Tags](https://abp.io/docs/latest/Modules/Cms-Kit/Tags), and [Reactions](https://abp.io/docs/latest/Modules/Cms-Kit/Reactions) features as enabled):

| Blog | Blog Post  |
|------------------------ |-----------------------|
| ![](assets/images/blogs.jpg) | ![](assets/images/blog-detail.jpg)  |

### Products pages

*Our Team* and *Products* uses the [Pages Feature](https://abp.io/docs/latest/Modules/Cms-Kit/Pages) of the CMS Kit Module (with dynamic content, styles & scripts):

| products/abp-commercial | products/aspnet-zero  |
|------------------------ |-----------------------|
| ![](assets/images/products-abp-commercial.png) | ![](assets/images/products-aspnetzero.png)  |

