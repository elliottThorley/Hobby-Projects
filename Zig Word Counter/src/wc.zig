const std = @import("std");
const fs = std.fs;
const io = std.io;
const mem = std.mem;
const testing = std.testing;
const debug = std.debug;
const zig = std.zig;

const StringHashMap = std.StringArrayHashMap;


pub const PrintFn = struct {
    print: fn(comptime fmt: []const u8, args: anytype) void
};

// If you need to allocate resorces, you will, pass the allocator around.
var alloc = testing.allocator;

/// Read the whole file into a buffer and return that buffer,
/// don't free whatever you have used to allocate the buffer until after you have used it.
///
/// Remember to close the file.
///
///                                   This means any error can be returned
pub fn readToString(path: []const u8) anyerror![]u8 {
    // Do stuff...
    var file = try std.fs.cwd().openFile(path, .{});
    defer file.close();
    const buffer_size = 100000;
    const file_buffer = try file.readToEndAlloc(alloc, buffer_size);
    return file_buffer;
}

/// You must return the StringHashMap that contians all the words and counts.
///
/// Remember don't free any of the keys or values of this map, don't free the map either.
pub fn wc(lines: []const u8) anyerror!StringHashMap(usize) {
    // Do stuff...
    var hashMap = StringHashMap(usize).init(alloc);

    var i: usize=0;
    var ii: usize=0;
    for (lines) |letter|{
        //you reached the end of the word
        if(letter==' ' or letter=='\n' or letter=='\r'){
            if(i!=ii){
                var t: usize = hashMap.get(lines[i..ii]) orelse 0;
                try hashMap.put(lines[i..ii],t+1);
                i=ii+1;
            }
            else{
                i=ii+1;
            }
        }
        //if this is part of the above if, it cuts off the last letter, if you take off the -1, it never occurs :(
        else if(ii==lines.len-1){
            if(i!=ii){
            ii=ii+1;
            var t: usize = hashMap.get(lines[i..ii]) orelse 0;
            try hashMap.put(lines[i..ii],t+1);
            i=ii+1;
            }
        }
        ii=ii+1;
    }
    
    return hashMap;
}

/// Each word must be printed like `print.print("{s}, {}\n", word, count)` and the total
/// `print.print("{s} {}\n", path, total)`.
pub fn printWordCount(word_map: *const StringHashMap(usize), path: []const u8, comptime print: PrintFn) void {
    // Do stuff...
    var it=word_map.iterator();
    var counter:usize=0;
    std.debug.print("\n",.{});
    while (it.next()) |entry|{
        counter=counter+entry.value_ptr.*;
        if(entry.value_ptr.*>1){
            print.print("{s} {}\n",.{entry.key_ptr.*,entry.value_ptr.*});
        }
    }
    print.print("\n{s} {d}\n",.{path,counter});
}