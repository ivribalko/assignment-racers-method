### Changes to the original method:

#### Readability:
- var keyword.
###### Reason: aligns code more nicely and reduces the variativity.
- static keyword.
###### Reason: static keyword on a method shows that it is pure and doesn't change class state.
- Method name.
###### Reason: the name is too generic, method name should give hints on what's happening inside of it.
- Method length.
###### Reason: the method is too long hindering the clear overlook of the logic that is being carried out inside of it.
- Variable names.
###### Reason: the name is too generic or misleading, the name should give hints on the rightful purpose of the variable.
- IReadOnly*.
###### Reason: using IReadOnly* will immediately give a hint that the collection is not modified which is vital in writing infallible code.
- Unnecessary comments.
###### Reason: code comments that duplicate the code behaviour interrupt the reading flow and simplicity. It's almost always possible to reorganize code in such manner that it doesn't require comments.
- No IndexOf().
###### Reason: should use Contains(); even if IndexOf() is faster the difference is neglectable.
- foreach.
###### Reason: for loop hinders readability; although previously in Unity foreach would create unnecessary allocations this is not the case anymore.

#### Performance:
- Less loops.
###### Reason: loops tend to increase the complexity of code very rapidly.
- Less conditions.
###### Reason: unnecessarily complicates the program execution and code reading.
- No new lists.
###### Reason: every new list is a new array hence new chunk of memory allocated and CPU cycles wasted.
- No double check collision.
###### Reason: collisions are supposed to be commutative meaning that a.collides(b) == b.collides(a) so we only need to check it once. The code has been rewritten however this should verified on the actual Racer implementation.
- No unused variables.
###### Reason: unnecessarily complicates the program execution and code reading.
- use ref instead of copy.
###### Reason: it's unnecessary to copy one collection to another; if the copied collection does not change it's possible to just use the reference to it.
- Store array elements into local variables.
###### Reason: accessing an element in an array takes time every execution.
- Store mathematical operations results into local variables.
###### Reason: repeating the same operation for producing the same result is inefficient.

### TODO:
- Add unit tests.
###### Reason: unit tests is number one tool in preventing any type of misfortune may some refactoring happen to a class.
- Add unit tests for no allocations.
###### Reason: code that runs every Update should be tested for having no allocations and this ability has (almost?) shipped for Unity.
- LINQ could be added but requires double check.
###### Reason: although LINQ reads nicely its performance and IL2CPP code generation issues should be considered when using it.
