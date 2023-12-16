using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class ContentBasedRecommendation
    {
        public ContentBasedRecommendation() { }
        // Phương thức để tính toán sự tương đồng giữa các sản phẩm
        public static double CalculateSimilarity(SanPham item1, SanPham item2)
        {
            // Thực hiện tính toán sự tương đồng dựa trên thuộc tính của sản phẩm
            // Có thể sử dụng các phương pháp khác nhau như Cosine Similarity, Jaccard Similarity, v.v.

            // Ví dụ: Sử dụng Cosine Similarity giữa mô tả của sản phẩm
            double similarity = CalculateCosineSimilarity((decimal)item1.GiaTien, (decimal)item2.GiaTien);

            return similarity;
        }

        // Phương thức để tính toán Cosine Similarity giữa hai chuỗi
        private static double CalculateCosineSimilarity(decimal value1, decimal value2)
        {
            // Thực hiện tính toán Cosine Similarity
            // Có thể sử dụng thư viện hoặc triển khai một số thuật toán như TF-IDF và Cosine Similarity từ đầu.
            string text1 = value1.ToString();
            string text2 = value2.ToString();
            // Ví dụ đơn giản:
            var vector1 = text1.Split(' ').Select(term => term.ToLowerInvariant()).Distinct();
            var vector2 = text2.Split(' ').Select(term => term.ToLowerInvariant()).Distinct();

            var intersection = vector1.Intersect(vector2).Count();
            var union = vector1.Count() + vector2.Count() - intersection;

            return (double)intersection / union;
        }
    }
}
