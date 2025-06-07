using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Userdata : MonoBehaviour
{

    public bool _isDemo;

    public static Userdata Instance;

    public LoginResponse _User;

    public string _WallExp_Token;

    [System.Serializable]
    public class LoginResponse
    {
        public string status;
        public string message;
        public Data data;
    }

    [System.Serializable]
    public class Data
    {
        public User user;
        public Profile profile;
        public Currency currency;
        public Booster booster;
        public CurrentWare current_ware;
        public string access_token;
    }

    [System.Serializable]
    public class User
    {
        public string name;
        public string email;
        public string type;
        public string birthdate;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Profile
    {
        public int id;
        public int user_id;
        public string enterprise_id;
        public string gender;
        public string eye_problem;
        public string birthdate;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Currency
    {
        public int gold;
        public int gem;
        public int vision_point;
    }

    [System.Serializable]
    public class Booster
    {
        public int booster1;
        public int booster2;
        public int booster3;
        public int booster4;
    }

    [System.Serializable]
    public class CurrentWare
    {
        public int current_hat;
        public int current_body;
        public int current_weapon;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<int> _InventroyStore;

    public RewardResponse _RewardData;

    [Serializable]
    public class RewardResponse
    {
        public string status;
        public int code;
        public List<ProductData> data;
    }

    [Serializable]
    public class ProductData
    {
        public int id;
        public string name;
        public int wellplace_brand_id;
        public int wellplace_category_id;
        public string product_image;
        public string description;
        public string condition;
        public int point;
        public string code_prefix;
        public int code_length;
        public int code_qty;
        public int limit_time;
        public int? time_left;
        public bool? online;
        public string start_date;
        public string end_date;
        public string created_at;
        public string updated_at;
        public string deleted_at;
        public int status;
        public string code;
        public int quota_center;
        public int min_balance;
        public bool? email_notification;
        public bool? send_email_flag;
        public string show_code;
        public int type_code;
        public int? clone_id;
        public string app_link;
        public string reason;
        public int created_by;
        public int updated_by;
        public int? deleted_by;
        public int? ticket_id;
        public string promotion_img;
        public string promotion;
        public float? amount;
        public string code_aurora;
        public string brands_name;
        public string brand_image;
        public int? count_redeem;
    }

    public CategoryResponse _RewardCategoryData;

    [System.Serializable]
    public class CategoryResponse
    {
        public string status;
        public int code;
        public List<CategoryData> data;
    }

    [System.Serializable]
    public class CategoryData
    {
        public int id;
        public string name;
        public string default_image;
        public string active_image;
        public DateTime created_at;
        public DateTime updated_at;
        public DateTime? deleted_at;
        public int status;
    }

    public ApiResponse _RewardGetBrand;

    [System.Serializable]
    public class ApiResponse
    {
        public string status;
        public int code;
        public List<CustomerData> data;
    }

    [System.Serializable]
    public class CustomerData
    {
        public int id;
        public string name;
        public int wellplace_category_id;
        public string default_image;
        public string created_at;
        public string updated_at;
        public string deleted_at;
        public int status;
        public int company_id;
        public int order_list;
        public string pay_type;
    }

    public RewardResponse_OK _RewardResponse_OK;

    [System.Serializable]
    public class RewardResponse_OK
    {
        public string status;
        public RewardData data;
    }

    [Serializable]
    public class RewardData
    {
        public RewardDetail reward_detail;
        public int used_point;
        public int current_vision_point;
    }

    [Serializable]
    public class RewardDetail
    {
        public int wellplace_redeem_transaction_id;
        public string unique_key;
        public string product_code;
        public string product_qrcode;
        public string product_barcode;
        public string wellplace_show_code;
        public int wellplace_product_id;
        public string name;
        public int wellplace_brand_id;
        public int wellplace_category_id;
        public string product_image;
        public string description;
        public string condition;
        public string promotion;
        public int point;
        public int price;
        public int cost;
        public string code_prefix;
        public int code_length;
        public int code_qty;
        public int limit_time;
        public int time_left;
        public string start_date;
        public string end_date;
        public string app_link;
        public string created_at;
        public string updated_at;
        public string wellplace_brand_image;
    }

    public RedeemResponse _RedeemResponse;

    [Serializable]
    public class RedeemResponse
    {
        public string status;
        public int code;
        public List<RedeemItem> data;
    }

    [Serializable]
    public class RedeemItem
    {
        public int wellplace_redeem_transaction_id;
        public string unique_key;
        public string product_code;
        public string product_qrcode;
        public string product_barcode;
        public string wellplace_show_code;
        public int wellplace_product_id;
        public string name;
        public int wellplace_brand_id;
        public int wellplace_category_id;
        public string product_image;
        public string description;
        public string condition;
        public string promotion;
        public int point;
        public int price;
        public int cost;
        public string code_prefix;
        public int code_length;
        public int code_qty;
        public int limit_time;
        public int time_left;
        public string start_date;
        public string end_date;
        public string created_at;
        public string updated_at;
        public string wellplace_brand_image;
        public int type_code;
        public string product_redirect;
        public string app_link;
    }




    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int playerScore;
    }

    [System.Serializable]
    public class LevelData
    {
        public int level_id;
        public int score;
        public int stars;
        public bool isUnlock;
        public List<LeaderboardEntry> Leaderboard;
    }

    [System.Serializable]
    public class WorldData
    {
        public int id;
        public string name;
        public int sumStars;
        public bool isUnlock;
        public List<LevelData> level;
    }

    [System.Serializable]
    public class GameWorldData
    {
        public List<WorldData> world;
    }

    public GameWorldData _WorldData;
}
