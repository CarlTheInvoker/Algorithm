# Segment tree

Segement tree is a tree-like data strucutre used to store data in the interval. So for a segment tree node, it contains:

- An interval \[Start , end)
- Some data associated with the interval

The root node is the whole interval, and for a node, the interval of its children are the intervals being evenly split. So `node.LeftChild.Start == node.Start && node.LeftChild.End == (node.Start + node.End) / 2`, and similiarly, it is easy to get the interval of the right child.

Segment tree is extremly usefully when the operation on the interval is something like:

- Update all data of some interval to the same value: normal segment tree
- Do the same operation in all data of some interval: lazy segment tree

Based on the child definition, the depth of a segment tree is `O(log(root.End - root.Start))`, and the `Query / Add / Remove` operation can be handled in `O(log(root.End - root.Start))`.
