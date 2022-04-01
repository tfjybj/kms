/*
 * 创建人；邓礼梅
 * 版权所有：dlm
 * 说明:  可编辑的表格
 */
$(function () {    // 相当于在页面中的body标签加上onload事件
    var objTD;
    var oldText;
    var input;
    var newText;
    var input_blur;
    var jianzhi;
    var input_keydown;
    var caid;

   
    // 给教室加上click函数
    $(".caname").click(function () {
        objTD = $(this);
        GetTableValue("ChangeCaName.ashx?caname=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);

            //限制只能输入中文
            //if (LimitOnlyChn() === false) {
            //    input_blur.trigger("focus").trigger("select");   // 文本框全选   
            //    $("#test").text(oldText);
            //    return;
            //} else {
            //    $("#test").text("");
            //    objTD.html(newText);
            //}

            //InputBlur("ChangeCaName.ashx?caname=");
            processSpelChar();
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入中文
            if (LimitOnlyChn() === false) {
                input_blur.trigger("focus").trigger("select");   // 文本框全选    
                return;

            } else {
                $("#test").text("");
                objTD.html(oldText);
            }
            Keydown("ChangeCaName.ashx?caname=");
        });
    });

    // 至少使用人数
    $(".minUseNumber").click(function () {
        objTD = $(this);

        GetTableValue("ChangeMinUseNumber.ashx?minUseNumber=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);

            //限制只能输入数字
            //if (LimitOnlyNum() === false) {
            //    input_blur.trigger("focus").trigger("select");   // 文本框全选   
            //    $("#test").text(oldText);
            //    return;
            //} else {
            //    $("#test").text("");
            //    objTD.html(oldText);
            //}
            //InputBlur("ChangeMinUseNumber.ashx?minUseNumber=");

             chkPrice(this);//只能输入数字

            
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            Keydown("ChangeMinUseNumber.ashx?minUseNumber=");
        });
    });

    //会议开始前*分钟取钥匙
    $(".beforeTakeKey").click(function () {
        objTD = $(this);
        GetTableValue("ChangeBeforeTakeKey.ashx?beforeTakeKey=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            InputBlur("ChangeBeforeTakeKey.ashx?beforeTakeKey=");
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            Keydown("ChangeBeforeTakeKey.ashx?beforeTakeKey=");
        });
    });

    //会议结束前*分钟还钥匙
    $(".afterReturnKey").click(function () {
        objTD = $(this);
        GetTableValue("ChangeAfterReturnKey.ashx?afterReturnKey=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            InputBlur("ChangeAfterReturnKey.ashx?afterReturnKey=");
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            Keydown("ChangeAfterReturnKey.ashx?afterReturnKey=");
        });
    });

    //会议室使用时间上限
    $(".upperTime").click(function () {
        objTD = $(this);
        GetTableValue("ChangeUpperTime.ashx?upperTime=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            InputBlur("ChangeUpperTime.ashx?upperTime=");
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            Keydown("ChangeUpperTime.ashx?upperTime=");
        });
    });

    //会议室使用时间下限
    $(".lowerTime").click(function () {
        objTD = $(this);
        GetTableValue("ChangeLowerTime.ashx?lowerTime=");

        // 文本框失去焦点时重新变为文本
        input.blur(function () {
            newText = $(this).val(); // 修改后的名称
            input_blur = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            InputBlur("ChangeLowerTime.ashx?lowerTime=");
        });

        // 在文本框中按下键盘某键
        input.keydown(function (event) {
            jianzhi = event.keyCode;
            input_keydown = $(this);
            //限制只能输入数字
            if (LimitOnlyNum() === false) {
                return;
            }
            Keydown("ChangeLowerTime.ashx?lowerTime=");
        });
    });

    ////审批人
    //$(".approver").click(function () {
    //    objTD = $(this);
    //    GetTableValue("ChangeCaName.ashx?caname=");

    //    // 文本框失去焦点时重新变为文本
    //    input.blur(function () {
    //        newText = $(this).val(); // 修改后的名称
    //        input_blur = $(this);
    //        InputBlur("ChangeCaName.ashx?caname=");
    //    });

    //    // 在文本框中按下键盘某键
    //    input.keydown(function (event) {
    //        jianzhi = event.keyCode;
    //        input_keydown = $(this);
    //        Keydown("ChangeCaName.ashx?caname=");
    //    });
    //});


    // 给页面中的标签加上click函数
    function GetTableValue(_changeUrl, _messageBox) {
        $("#test").text("");
        oldText = $.trim(objTD.text());  // 保存老的类别名称        
        input = $("<input type='text' value='" + oldText + "' />"); // 文本框的HTML代码
        objTD.html(input);   // 当前td的内容变为文本框
        // 设置文本框的点击事件失效
        input.click(function () {
            return false;
        });
        // 设置文本框的样式
        input.css("border-width", "0px");  //边框为0
        input.height(objTD.height());   //文本框的高度为当前td单元格的高度
        input.width(objTD.width());    // 宽度为当前td单元格的宽度
        input.css("font-size", "14px");    // 文本框的内容文字大小为14px
        input.css("text-align", "center");   //  文本居中
        input.trigger("focus").trigger("select");   // 全选
    }

    // 文本框失去焦点时重新变为文
    function InputBlur(_changeUrl) {
        // 当老的类别名称与修改后的名称不同的时候才进行数据的提交操作
        if (oldText != newText) {
            // 获取该类别名所对应的ID(序号)            
            var id = $.trim(objTD.prevAll().text());
            var splitId = id.split(' ');
            var caid = splitId[splitId.length - 1];
            // AJAX异步更改数据库
            var url = "../HandlingProcedure/" + _changeUrl + encodeURI(encodeURI(newText)) + "&caid=" + caid + "&t=" + new Date().getTime();
            $.get(url, function (data) {
                if (data == "false") {
                    $("#test").text("修改失败,请检查是否符合规范！");
                    input_blur.trigger("focus").trigger("select");   // 文本框全选           
                } else {
                    $("#test").text("");
                    objTD.html(newText);
                }
            });
        } else {
            // 前后文本一致,把文本框变成标签
            objTD.html(newText);
        };
    }

    // 在文本框中按下键盘某键
    function Keydown(_changeUrl) {
        switch (jianzhi) {
            case 13: // 按下回车键 ,把修改后的值提交到数据库
                // $("#test").text("您按下的键值是: " + jianzhi);
                var newText = input_keydown.val(); // 修改后的名称
                // 当老的类别名称与修改后的名称不同的时候才进行数据的提交操作
                if (oldText != newText) {
                    // 获取该类别名所对应的ID(序号) 
                    var id = $.trim(objTD.prevAll().text());
                    var splitId = id.split(' ');
                    var caid = splitId[splitId.length - 1];
                    // AJAX异步更改数据库
                    var url = "../HandlingProcedure/" + _changeUrl + encodeURI(encodeURI(newText)) + "&caid=" + caid + "&t=" + new Date().getTime();

                    $.get(url, function (data) {
                        if (data == "false") {
                            $("#test").text("修改失败,请检查是否符合规范！");
                            input_keydown.trigger("focus").trigger("select");   // 文本框全选
                        } else {
                            $("#test").text("");
                            objTD.html(newText);
                        }
                    });
                } else {
                    // 前后文本一致,把文本框变成标签
                    objTD.html(newText);
                }
                break;
            case 27: // 按下Esc键, 取消修改,把文本框变成标签
                $("#test").text("");
                objTD.html(oldText);
                break;
        }
    }

    //只能输入数字
    function LimitOnlyNum() {
        if (newText == "" || isNaN(newText)) {
            $("#test").text("请填写数字");
            //input_blur.trigger("focus").trigger("select");   // 文本框全选    
            return fasle;
        }
    }


    //限制只能输入数字
    function chkPrice(obj) {
        obj.value = obj.value.replace(/[^\d.]/g, "");
        //必须保证第一位为数字而不是. 
        obj.value = obj.value.replace(/^\./g, "");
        //保证只有出现一个.而没有多个. 
        obj.value = obj.value.replace(/\.{2,}/g, ".");
        //保证.只出现一次，而不能出现两次以上 
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    }
    function chkLast(obj) {
        // 如果出现非法字符就截取掉 
        if (obj.value.substr((obj.value.length - 1), 1) == '.')
            obj.value = obj.value.substr(0, (obj.value.length - 1));
    }
    
    //只能输入汉字
    var LimitOnlyChn = function () {
        var text = newText;
        //匹配只包含中文和、和A-Z和0-9 \u4e00-\u9fa5表示中文正则，\u3001表示、正则；\d表示数字 。 以中文或A-Z或0-9或、开头都可以。
        var reg = /^[\u4e00-\u9fa5\u3001\A-\Z\d]+$/;
        if (reg.test(text)) {
        } else {
            $("#test").text("只能是中文、字母和数字哦！");
         
            return fasle;
        }
    }
});

// 屏蔽Enter按键
$(document).keydown(function (event) {
    switch (event.keyCode) {
        case 13: return false;
    }
});
