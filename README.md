# BlockGame


## Code Style

#### Documentation:
  * Properly documented public and protected:
    - Summary
    - Params
    - Returns or Value
    - Exception
    - Example
  * Slightly documented private if needed

#### Order ( by StyleCop ):
  * Within class:
    - Fields
    - Constructors
    - Destructors
    - Delegates
    - Events
    - Enums
    - Interface implementation ( or in other part )
    - Properties
    - Indexers
    - Methods
    - Structs
    - Classes
  * In each group by access:
    - public
    - internal
    - protected internal
    - protected
    - private
  * In each access group:
    - static
    - non-static
  * Then by readability:
    - readonly
    - non-readonly

#### Naming convention:
  * CamelCase
  * Starts with capital:
    - Properties
    - Public variables
  * Starts with lowerCase:
    - Local variaples
    - Parameters
  * Starts with underscore:
    - Private variables

#### Other:
  * Partial class:
    - Part implementing main functionality
    - Part implementing interface
  * KISS <3


## Plan

#### To implement:
  * Renderer
  * Input handler
  * File handler
  * Game logic
  * Math functions

##### Renderer:
  * Renderer interface:
    - Render chunks
    - Render GUI ( inventory, status, etc. )
    - Render menu
  * Optimalization for rendering ( hidden ):
    - Render only in front of user
    - Renter only up or bottom of block
    - Render only two sides of block?
    - Render only blocks without surounding
  * Textures:
    - Loader for atlas
  * Model

##### Input handler:
  * Input for screen rotation
  * Cursor handling:
    - Menu
    - Inventory
  * Keyboard

##### File handler:
  * Interface for storing data
  * Own fileformat:
    - Random access

##### Game logic:
  * Menu
  * Player:
    - Jump
  * World:
    - Chunks
  * Items:
    - Crafting
    - Usage
    - Durability

##### Math functions:
  * Noise functions
