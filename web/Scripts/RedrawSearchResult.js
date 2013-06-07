$(function()   
{   
    var hideDelay = 500;     
    var currentID;   
    var hideTimer = null;   
  
    // One instance that's reused to show info for the current person   
    var container = $('<div id="SRToolTips"></div>');   
  
    $('body').append(container);   
  
    $('table.dataTable tr a').live('mouseover', function()   
    {   
        // format of 'rel' tag: pageid,personguid   
        var settings = $(this).attr('rel').split(',');   
        var URLID = settings[0];   
        tips = settings[1];   
  
        if (hideTimer)   
            clearTimeout(hideTimer);   
  
        var pos = $(this).offset();   
        var width = $(this).width();   
        container.css({   
            left: (pos.left + width) + 'px',   
            top: pos.top - 3 + 'px'  
        });   
        container.append(tips);
        container.css('display', 'block');
    });   
  
    $('table.dataTable tr a').live('mouseout', function ()
    {   
        if (hideTimer)   
            clearTimeout(hideTimer);   
        hideTimer = setTimeout(function()   
        {   
            container.css('display', 'none');   
        }, hideDelay);   
    });   
  
    // Allow mouse over of details without hiding details   
    $('table.dataTable tr a').mouseover(function ()
    {   
        if (hideTimer)   
            clearTimeout(hideTimer);   
    });   
  
    // Hide after mouseout   
    $('table.dataTable tr a').mouseout(function ()
    {   
        if (hideTimer)   
            clearTimeout(hideTimer);   
        hideTimer = setTimeout(function()   
        {   
            container.css('display', 'none');   
        }, hideDelay);   
    });   
});