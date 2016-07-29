# LinkInterfaceMenuFramework
A framework for a composition-oriented generic component-based Menu system

<img src ="/Header.png">

## Overview

While working on a prototype for Unity and finding myself in the need of making menus for it, I remembered how I have written menu code in the past. Like most novice programmers, I would write a script that would have a bunch of references to the physical buttons on the scene, have a stack for navigating down the menu, have the navigation code by checking for directional input and confirmation on update and so on.

Given that, this time I wanted to do it better. Not just as a matter of personal pride but because I wanted to be able to reuse a large majority of code and components over my menus, with the little remainder used to write subclasses that implement specific behavior for those special cases. There were a few other goals I wanted to fulfill:

- To eliminate needing to write or handle navigation manually when using the framework. I wanted this to be automated. I wanted to remove the dependency between the menus and how they are navigated.
- To consolidate the different ways menus buttons are used among different components. For example sometimes a menu button may open another window, or execute a function. Or lead on to modifying a property. Etc.
- Implement the framework through the Observer pattern using event-driven programming, which would allow me to easily extend functionality. (I pass in events around rather than doing direct function calls)

As of now I strongly believe I have fulfilled that promise with the way the framework is as of now. While the prototype of it has been currently implemented in Unity through C#, it could be implemented and used in any ECS engine with a robust, customizable editor. I will be going over the design and classes, leaving language-specific implementation details as an exercise to the reader.

## Notes
- [Stratus Framework](https://github.com/Azurelol/StratusFramework): It uses the event system heavily in order to follow the observer pattern.
The framework has the following dependencies:
- [Ludiq Reflection](https://github.com/lazlo-bonin/unity-reflection): It uses a coupple of its inspector drawers (UnityVariable, UnityMethod) for some link behavior.

## Documentation | [Web](https://docs.google.com/document/d/1ag4u-xh_ymxL3EFth453XrcqRoySuoF3eb9-AhKfaLA/edit?usp=sharing)

## Changelog
- (7/28/2016) Repository uploaded! Cleaned up the tentatively-named framework so it is truly modular. Given further thinking, decided to remove