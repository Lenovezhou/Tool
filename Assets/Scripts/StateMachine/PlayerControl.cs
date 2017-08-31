﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

///@brief
///文件名称:PlayerControl
///功能描述:
///数据表:
///作者:YuXianQiang
///日期:#CreateTime#
///R1:
///修改作者:
///修改日期:
///修改理由:

namespace HuangDong.Yu
{
    public class PlayerControl : MonoBehaviour
    {
		public Camera maincamera;
        void Update()
        {
            float speed = 0.4f;
            float h = Input.GetAxis("Horizontal")*speed;
            float v = Input.GetAxis("Vertical")*speed;

            //transform.Translate(h, 0, v);
            Vector3.MoveTowards(transform.position, transform.position += new Vector3(h, 0, v), Time.deltaTime*speed);
			if (maincamera != null)
			{
				maincamera.transform.position = Vector3.Lerp(maincamera.transform.position
					,new Vector3(transform.position.x
						,maincamera.transform.position.y
						,transform.position.z),0.25f);
			}
		}
    }
}