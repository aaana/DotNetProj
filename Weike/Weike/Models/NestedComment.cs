using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class NestedComment
    {
        public CommentData commentData { get; set; }
        public List<NestedComment> nestedComments { get; set; }

        public NestedComment(CommentData commentData, List<NestedComment> nestedComments)
        {
            this.commentData = commentData;
            this.nestedComments = nestedComments;
        }

        static public List<NestedComment> getAllCommentsByWeikeId(int weike_id)
        {
            List<NestedComment> result = new List<NestedComment>();
            List<CommentData> cdList = CommentDB.FindCommentDataByWeikeId(weike_id);
            List<CommentData> rootComments = new List<CommentData>();
        
           
            foreach(CommentData cd in cdList)
            {
                if (cd.comment.parent == 0)
                {
                    rootComments.Add(cd);
                }
            }

            foreach(CommentData cd in rootComments)
            {
                int comment_id = cd.comment.comment_id;
                NestedComment nestedComment = new NestedComment(cd, getNestedComments(cdList, comment_id));
                result.Add(nestedComment);
            }
            return result;
        }

        static private List<NestedComment> getNestedComments(List<CommentData> cdList, int comment_id)
        {
            Boolean isLast = true;
            List<NestedComment> nestedComments = new List<NestedComment>();
            foreach(CommentData cd in cdList)
            {
                if (cd.comment.parent == comment_id)
                {
                    isLast = false;
                    break;
                }
            }
            if (isLast)
            {
                return nestedComments;
            }
            else
            {
                foreach (CommentData cd in cdList)
                {
                    if (cd.comment.parent == comment_id)
                    {
                        NestedComment nestedComment = new NestedComment(cd,getNestedComments(cdList,cd.comment.comment_id));
                        nestedComments = new List<NestedComment>();
                        nestedComments.Add(nestedComment);
                    }
                }
                return nestedComments;
            }
        }
    }
}