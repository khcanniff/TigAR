# The Official TigAR CS Development C# Style Guide 

This style guidewas taken mostly from raywanderlich and is different from others
you may see, because the focus is centered on readability for print and the web. 
They created this style guide to keep the code in our tutorials consistent.  
For TigAR purpose: dont worry about following this guide religiously.  

The overarching goals are **conciseness**, **readability** and **simplicity**. Also, this guide is written to keep **Unity** in mind. 

## Inspiration

This style guide is based on C# and Unity conventions. 

## Table of Contents

- [Nomenclature](#nomenclature)
  + [Namespaces](#namespaces)
  + [Classes & Interfaces](#classes--interfaces)
  + [Methods](#methods)
  + [Fields](#fields)
  + [Parameters](#parameters--parameters)
  + [Delegates](#delegates--delegates)
  + [Events](#events--events)
  + [Misc](#misc)
- [Declarations](#declarations)
  + [Access Level Modifiers](#access-level-modifiers)
  + [Fields & Variables](#fields--variables)
  + [Classes](#classes)
  + [Interfaces](#interfaces)
- [Spacing](#spacing)
  + [Indentation](#indentation)
  + [Line Length](#line-length)
  + [Vertical Spacing](#vertical-spacing)
- [Brace Style](#brace-style)
- [Switch Statements](#switch-statements)
- [Language](#language)
- [Credit](#credits)


## Nomenclature

On the whole, naming should follow C# standards.

### Namespaces

Namespaces are all **PascalCase**, multiple words concatenated together, without hyphens ( - ) or underscores ( \_ ). The exception to this rule are acronyms like GUI or HUD, which can be uppercase:

**BAD**:

```csharp
com.raywenderlich.fpsgame.hud.healthbar
```

**GOOD**:

```csharp
RayWenderlich.FPSGame.HUD.Healthbar
```

### Classes & Interfaces

Written in **PascalCase**. For example `RadialSlider`. 

### Methods

Methods are written in **PascalCase**. For example `DoSomething()`. 

### Fields

All non-static fields are written **camelCase**. Per Unity convention, this includes **public fields** as well.

For example:

```csharp
public class MyClass 
{
    public int publicField;
    int packagePrivate;
    private int myPrivate;
    protected int myProtected;
}
```

**BAD:**

```csharp
private int _myPrivateVariable
```

**GOOD:**

```csharp
private int myPrivateVariable
```

Static fields are the exception and should be written in **PascalCase**:

```csharp
public static int TheAnswer = 42;
```

### Parameters

Parameters are written in **camelCase**.

**BAD:**

```csharp
void DoSomething(Vector3 Location)
```
**GOOD:**

```csharp
void DoSomething(Vector3 location)
```

Single character values are to be avoided except for temporary looping variables.

### Delegates

Delegates are written in **PascalCase**.

When declaring delegates, DO add the suffix **EventHandler** to names of delegates that are used in events. 

**BAD:**

```csharp
public delegate void Click()
```
**GOOD:**

```csharp
public delegate void ClickEventHandler()
```  

DO add the suffix **Callback** to names of delegates other than those used as event handlers.

**BAD:**

```csharp
public delegate void Render()
```
**GOOD:**

```csharp
public delegate void RenderCallback()
```  

### Events

Events should be written in **PascalCase**. Never prefix events with a prefix like **On**.

**BAD:**

```csharp
public static event CloseCallback OnClose;
```  

**GOOD:**

```csharp
public static event CloseCallback Close;
```

### Misc

In code, acronyms should be treated as words. For example:

**BAD:**

```csharp
XMLHTTPRequest
String URL
findPostByID
```  

**GOOD:**

```csharp
XmlHttpRequest
String url
findPostById
```

## Declarations

### Access Level Modifiers

Access level modifiers should be explicitly defined for classes, methods and member variables.

### Fields & Variables

Prefer single declaration per line.

**BAD:**

```csharp
string username, twitterHandle;
```

**GOOD:**

```csharp
string username;
string twitterHandle;
```

### Classes

Exactly one class per source file, although inner classes are encouraged where scoping appropriate.

### Interfaces

All interfaces should be prefaced with the letter **I**. 

**BAD:**

```csharp
RadialSlider
```

**GOOD:**

```csharp
IRadialSlider
```

## Spacing

### Indentation

Indentation should be done using **tabs** — never spaces.  

#### Blocks

**BAD:**

```csharp
for (int i = 0; i < 10; i++) 
{
    Debug.Log("index=" + i);
}
```

**GOOD:**

```csharp
for (int i = 0; i < 10; i++) 
{
  Debug.Log("index=" + i);
}
```

#### Line Wraps

Indentation for line wraps should use **4 spaces** (not the default 8):

**BAD:**

```csharp
CoolUiWidget widget =
        someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

**GOOD:**

```csharp
CoolUiWidget widget =
    someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

### Line Length

Lines should be no longer than **100** characters long.

### Vertical Spacing

There should be exactly one blank line between methods to aid in visual clarity 
and organization. Whitespace within methods should separate functionality, but 
having too many sections in a method often means you should refactor into
several methods.


## Brace Style


**GOOD:**
```csharp
class MyClass {
    void DoSomething() {
        if (someTest) {
          // ...
        } else {
          // ...
        }
    }
}
```

**BAD:**
```csharp
class MyClass
{
    void DoSomething()
    {
        if (someTest)
        {
          // ...
        }
        else
        {
          // ...
        }
    }
}
```

Conditional statements are always required to be enclosed with braces,
irrespective of the number of lines required.

**BAD:**

```csharp
if (someTest)
    doSomething();  

if (someTest) doSomethingElse();
```

**GOOD:**

```csharp
if (someTest) 
{
    DoSomething();
}  

if (someTest)
{
    DoSomethingElse();
}
```
## Switch Statements

Switch-statements come with `default` case by default (heh). If the `default` case is never reached, be sure to remove it.

**DEFAULT GETS USED:**  
  
```csharp
switch (variable) 
{
    case 1:
        break;
    case 2:
        break;
    default:
        break;
}
```

**DEFAULT DOESN'T GET USED:**  
  
```csharp
switch (variable) 
{
    case 1:
        break;
    case 2:
        break;
}
```

## Language

Use US English spelling.

**BAD:**

```csharp
string colour = "red";
```

**GOOD:**

```csharp
string color = "red";
```

The exception here is `MonoBehaviour` as that's what the class is actually called.

## Credits

This style guide is a collaborative effort from the most stylish
raywenderlich.com team members:

- [Darryl Bayliss](https://github.com/DarrylBayliss)
- [Sam Davies](https://github.com/sammyd)
- [Mic Pringle](https://github.com/micpringle)
- [Brian Moakley](https://github.com/VegetarianZombie)
- [Ray Wenderlich](https://github.com/rwenderlich)
- [Eric Van de Kerckhove](https://github.com/BlackDragonBE)

